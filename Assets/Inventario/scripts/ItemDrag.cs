using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;
    RectTransform rectTransform;

    public float minDropDistance = 2f;
    public float maxDropDistance = 3f;
    private inventoryController invController;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        invController = inventoryController.Instance;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvasGroup.transform.lossyScale.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        slot dropSlot = eventData.pointerEnter?.GetComponent<slot>();
        if (dropSlot == null)
        {
            GameObject item = eventData.pointerEnter;
            if (item != null)
            {
                dropSlot = item.GetComponentInParent<slot>();
            }
        }
        slot originalSlot = originalParent.GetComponent<slot>();

        if (dropSlot != null)
        {
            if (dropSlot.currentItem != null)
            {
                item draggedItem = GetComponent<item>();
                item targedItem = dropSlot.currentItem.GetComponent<item>();

                if (draggedItem.ID == targedItem.ID)
                {
                    targedItem.AddtoStack(draggedItem.quantity);
                    originalSlot.currentItem = null;
                    Destroy(gameObject);
                }
                else
                {
                    dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                    originalSlot.currentItem = dropSlot.currentItem;
                    dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                    transform.SetParent(dropSlot.transform);
                    dropSlot.currentItem = gameObject;
                    GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
            }
            else
            {
                originalSlot.currentItem = null;
                transform.SetParent(dropSlot.transform);
                dropSlot.currentItem = gameObject;
                GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
        }
        else
        {
            if (!IsWithinInventory(eventData.position))
            {
                //drop the item
                DropItem(originalSlot);
            }
            else
            {
                transform.SetParent(originalParent);
                GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
        }
    }

    bool IsWithinInventory(Vector2 mousePosition)
    {
        RectTransform inventoryRect = originalParent.parent.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(inventoryRect, mousePosition);
    }

    void DropItem(slot originalslot)
    {
        item item = GetComponent<item>();
        int quantity = item.quantity;

        if (quantity > 1)
        {
            item.RemoveFromStack();
            transform.SetParent(originalParent);
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            quantity = 1;
        }
        else
        {
            originalslot.currentItem = null;
        }

        Transform PlayerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (PlayerTransform == null)
        {
            Debug.LogError("Missing''Player' 'tag");
            return;
        }

        Vector2 dropOffset = Random.insideUnitCircle.normalized * Random.Range(minDropDistance, maxDropDistance);
        Vector2 dropPosition = (Vector2)PlayerTransform.position + dropOffset;
        GameObject dropItem = Instantiate(gameObject, dropPosition, Quaternion.identity);
        item droppedItem = dropItem.GetComponent<item>();
        droppedItem.quantity = 1;

        if (quantity <= 1 && originalslot.currentItem == null)
        {
            Destroy(gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            SplitStack();
        }
        else if (eventData.button == PointerEventData.InputButton.Left && !eventData.dragging)
        {
            if (transform.parent != null && transform.parent.GetComponent<slot>() != null)
            {
                if (invController == null)
                    invController = inventoryController.Instance;

                invController?.MostrarEnSlotGrande(gameObject);
            }
        }
    }

    private void SplitStack()
    {
        item item = GetComponent<item>();
        if (item == null || item.quantity <= 1) return;

        int splitAmount = item.quantity / 2;
        if (splitAmount <= 0) return;

        item.RemoveFromStack(splitAmount);
        GameObject newItem = item.CloneItem(splitAmount);

        if (invController == null || newItem == null) return;

        foreach (Transform slotTransform in invController.inventoryPanel.transform)
        {
            slot slot = slotTransform.GetComponent<slot>();
            if (slot != null && slot.currentItem == null)
            {
                slot.currentItem = newItem;
                newItem.transform.SetParent(slot.transform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                return;
            }
        }

        item.AddtoStack(splitAmount);
        Destroy(newItem);
    }
}