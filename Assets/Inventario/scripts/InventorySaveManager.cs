using System.Collections.Generic;
using UnityEngine;

public class InventorySaveManager : MonoBehaviour
{
    public slot[] allSlots;         
    public itemdictionary itemDict;    


    public void SaveInventory()
    {
        SaveData data = new SaveData();
        data.inventoryData = new List<InventoryData>();

        for (int i = 0; i < allSlots.Length; i++)
        {
            if (allSlots[i].currentItem != null)
            {

                item itemComponent = allSlots[i].currentItem.GetComponent<item>();
                
                if (itemComponent != null)
                {
                    InventoryData slotData = new InventoryData
                    {
                        itemID = itemComponent.ID,
                        slotIndex = i
                    };
                    data.inventoryData.Add(slotData);
                }
            }
        }


        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("InventorySave", json);
        PlayerPrefs.Save();
        
        Debug.Log("¡Inventario guardado con éxito!");
    }


    public void LoadInventory()
    {

        foreach (slot s in allSlots)
        {
            if (s.currentItem != null)
            {
                Destroy(s.currentItem);
                s.currentItem = null;
            }
        }


        if (!PlayerPrefs.HasKey("InventorySave"))
        {
            Debug.Log("No hay datos de inventario guardados.");
            return;
        }

        string json = PlayerPrefs.GetString("InventorySave");
        SaveData data = JsonUtility.FromJson<SaveData>(json);


        foreach (InventoryData invData in data.inventoryData)
        {
            if (invData.slotIndex >= 0 && invData.slotIndex < allSlots.Length)
            {

                GameObject itemPrefab = itemDict.GetItemPrefab(invData.itemID);

                if (itemPrefab != null)
                {

                    GameObject newItem = Instantiate(itemPrefab, allSlots[invData.slotIndex].transform);
                    allSlots[invData.slotIndex].currentItem = newItem;
                    

                    newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
            }
        }

        Debug.Log("¡Inventario cargado y posicionado con éxito!");
    }
}