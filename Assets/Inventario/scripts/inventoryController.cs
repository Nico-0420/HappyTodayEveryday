using System.Collections.Generic;
using UnityEngine;

public class inventoryController : MonoBehaviour
{
    private itemdictionary itemdictionary;
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;

    // Usamos Awake para que el diccionario y los slots existan ANTES de que cualquier Start() intente cargar datos
    void Awake()
    {
        itemdictionary = FindAnyObjectByType<itemdictionary>();

        // Inicializamos los slots al arrancar el juego para que existan en la UI
        InitializeSlots();
    }

    void InitializeSlots()
    {
        // Limpiamos por si acaso hay slots viejos
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < slotCount; i++)
        {
            slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<slot>();
            
            // Opcional: si quieres mantener los items por defecto que tenías comentados
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
                    invData.Add(new InventoryData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex() });
                }
            }
        }
        return invData;
    }

    public void SetInventoryItems(List<InventoryData> InventoryData)
    {
        // Validación de seguridad por si la lista viene nula
        if (InventoryData == null) return;

        // Limpiar slots actuales
        foreach (Transform child in inventoryPanel.transform)
        {
            slot slot = child.GetComponent<slot>();
            if (slot != null) slot.currentItem = null;
            Destroy(child.gameObject);
        }

        // Volver a crear los slots vacíos base
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }

        // Colocar los items guardados en sus respectivos slots
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
                        slot.currentItem = item;
                    }
                }
            }
        }
    }
}