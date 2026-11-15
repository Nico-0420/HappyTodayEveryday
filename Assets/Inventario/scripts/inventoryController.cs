
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryController : MonoBehaviour
{

    private itemdictionary itemdictionary;
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;

    public static inventoryController Instance{get;private set;}

    void Awake()
    {
        if (Instance !=null && Instance!=this)
        {
            Destroy(gameObject);
            return;
        }

        Instance=this;
        itemdictionary = FindAnyObjectByType<itemdictionary>();

        InitializeSlots();
    }

    void InitializeSlots()
    {

        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < slotCount; i++)
        {
            slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<slot>();
           

            if (i < itemPrefabs.Length && itemPrefabs[i] != null)
            {
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = item;
            }
        }
    }

    public bool AddItem(GameObject itemPrefab)
    {
        item itemToAdd = itemPrefab.GetComponent<item>();
        if(itemToAdd==null)return false;

        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            slot slot = slotTransform.GetComponent<slot>();
            if (slot != null && slot.currentItem != null)
            {
                item slotItem = slot.currentItem.GetComponent<item>();
                if (slotItem != null && slotItem.ID == itemToAdd.ID)
                {
                    slotItem.AddtoStack();
                    return true;
                }

            }
        }

        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            slot slot = slotTransform.GetComponent<slot>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab, slot.transform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = newItem;
                return true;
            }
        }
        Debug.Log("Inventory is full");
        return false;
    }

    public List<InventoryData> GetInventoryItems()
    {
        List<InventoryData> invData = new List<InventoryData>();
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            slot slot = slotTransform.GetComponent<slot>();
            if (slot != null && slot.currentItem != null)
            {
                item item = slot.currentItem.GetComponent<item>();
                if (item != null)
                {
                    invData.Add(new InventoryData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex(),
                    quantity=item.quantity });
                }
            }
        }
        return invData;
    }

    public void SetInventoryItems(List<InventoryData> InventoryData)
    {

        if (InventoryData == null) return;

        foreach (Transform child in inventoryPanel.transform)
        {
            slot slot = child.GetComponent<slot>();
            if (slot != null) slot.currentItem = null;
            Destroy(child.gameObject);
        }

        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }

        foreach (InventoryData data in InventoryData)
        {
            if (data.slotIndex < slotCount)
            {
                Transform slotTransform = inventoryPanel.transform.GetChild(data.slotIndex);
                slot slot = slotTransform.GetComponent<slot>();
               
                if (itemdictionary != null)
                {
                    GameObject itemPrefab = itemdictionary.GetItemPrefab(data.itemID);
                    if (itemPrefab != null)
                    {
                        GameObject item = Instantiate(itemPrefab, slot.transform);
                        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                        item itemComponent = item.GetComponent<item>();
                        if (itemComponent != null && data.quantity > 1)
                        {
                            itemComponent.quantity = data.quantity;
                            itemComponent.UpdateQuantityDisplay();
                        }
                        slot.currentItem = item;
                    }
                }
            }
        }
    }
}