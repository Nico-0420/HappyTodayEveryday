
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

    [Header("Tamaño dentro del slot")]
    public bool fitToSlot = true;
    public float slotPadding = 12f;
    public Vector3 inventoryItemScale = new Vector3(2f, 2f, 2f);

    public static inventoryController Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        itemdictionary = FindAnyObjectByType<itemdictionary>();

        InitializeSlots();
    }



private void PlaceItemInSlot(GameObject itemObject, Transform slotTransform)
{
    RectTransform rect = itemObject.GetComponent<RectTransform>();

    if (rect != null)
    {
        rect.SetParent(slotTransform, false);

        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);

        rect.anchoredPosition = Vector2.zero;
        rect.localRotation = Quaternion.identity;

        // Mantener el tamaño deseado
        rect.sizeDelta = new Vector2(50f, 50f);

        rect.localScale = Vector3.one;

        Image img = itemObject.GetComponentInChildren<Image>();

        if (img != null)
        {
            img.preserveAspect = true;
        }
    }
    else
    {
        itemObject.transform.SetParent(slotTransform, false);
        itemObject.transform.localPosition = Vector3.zero;
        itemObject.transform.localRotation = Quaternion.identity;
        itemObject.transform.localScale = Vector3.one;
    }
}

    void InitializeSlots()
    {
        if (inventoryPanel == null || slotPrefab == null)
        {
            Debug.LogError("Falta asignar Inventory Panel o Slot Prefab.");
            return;
        }

        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < slotCount; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, inventoryPanel.transform);

            slot slot = newSlot.GetComponent<slot>();

            if (slot == null)
            {
                Debug.LogError("El Slot Prefab no tiene el componente slot.");
                continue;
            }

            if (i < itemPrefabs.Length && itemPrefabs[i] != null)
            {
                GameObject item = Instantiate(itemPrefabs[i]);

                PlaceItemInSlot(item, slot.transform);
                slot.currentItem = item;
            }
        }
    }

    public bool AddItem(GameObject itemPrefab)
    {
        if (itemPrefab == null)
        {
            Debug.LogError("El prefab del objeto está vacío.");
            return false;
        }

        item itemToAdd = itemPrefab.GetComponent<item>();

        if (itemToAdd == null)
        {
            Debug.LogError("El prefab no tiene el componente item en el objeto principal.");
            return false;
        }

        if (inventoryPanel == null)
        {
            Debug.LogError("Inventory Panel no está asignado.");
            return false;
        }

        Debug.Log("Intentando agregar objeto ID: " + itemToAdd.ID);


        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            slot slot = slotTransform.GetComponent<slot>();

            if (slot != null && slot.currentItem != null)
            {
                item slotItem = slot.currentItem.GetComponent<item>();

                if (slotItem != null && slotItem.ID == itemToAdd.ID)
                {
                    slotItem.AddtoStack();

                    Debug.Log("Objeto acumulado. Cantidad: " + slotItem.quantity);

                    return true;
                }
            }
        }

        // Buscar un espacio vacío
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            slot slot = slotTransform.GetComponent<slot>();

            if (slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab);

                PlaceItemInSlot(newItem, slot.transform);

                slot.currentItem = newItem;

                Debug.Log("Objeto agregado correctamente al slot: " +
                    slotTransform.GetSiblingIndex());

                return true;
            }
        }

        Debug.LogError("No hay espacios disponibles en el inventario.");

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
                    invData.Add(new InventoryData
                    {
                        itemID = item.ID,
                        slotIndex = slotTransform.GetSiblingIndex(),
                        quantity = item.quantity
                    });
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

            if (slot != null)
                slot.currentItem = null;

            Destroy(child.gameObject);
        }

        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }

        foreach (InventoryData data in InventoryData)
        {
            if (data.slotIndex >= 0 && data.slotIndex < slotCount)
            {
                Transform slotTransform = inventoryPanel.transform.GetChild(data.slotIndex);

                slot slot = slotTransform.GetComponent<slot>();

                if (itemdictionary != null)
                {
                    GameObject itemPrefab = itemdictionary.GetItemPrefab(data.itemID);

                    if (itemPrefab != null)
                    {
                        GameObject item = Instantiate(itemPrefab);

                        PlaceItemInSlot(item, slot.transform);

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


    public int CountItemByID(int itemID)
    {
        int total = 0;

        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            slot slot = slotTransform.GetComponent<slot>();

            if (slot != null && slot.currentItem != null)
            {
                item inventoryItem = slot.currentItem.GetComponent<item>();

                if (inventoryItem != null && inventoryItem.ID == itemID)
                {
                    total += inventoryItem.quantity;
                }
            }
        }

        return total;
    }
}
