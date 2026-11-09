using System.Collections.Generic;
using UnityEngine;

public class InventorySaveManager : MonoBehaviour
{
    public slot[] allSlots;             // Arrastra aquí todos los slots de tu inventario
    public itemdictionary itemDict;     // Referencia a tu script ItemDictionary

    // Método para conectar con el Botón de Guardar
    public void SaveInventory()
    {
        SaveData data = new SaveData();
        data.inventoryData = new List<InventoryData>();

        for (int i = 0; i < allSlots.Length; i++)
        {
            if (allSlots[i].currentItem != null)
            {
                // Asumimos que tu ítem tiene un componente o script que guarda su ID (ej. un script 'item')
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

        // Convertimos los datos a JSON y los guardamos en PlayerPrefs
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("InventorySave", json);
        PlayerPrefs.Save();
        
        Debug.Log("¡Inventario guardado con éxito!");
    }

    // Método para Cargar (puedes llamarlo al iniciar el juego)
    public void LoadInventory()
    {
        // 1. Limpiar el inventario actual primero para evitar duplicados
        foreach (slot s in allSlots)
        {
            if (s.currentItem != null)
            {
                Destroy(s.currentItem);
                s.currentItem = null;
            }
        }

        // 2. Verificar si existe un guardado previo
        if (!PlayerPrefs.HasKey("InventorySave"))
        {
            Debug.Log("No hay datos de inventario guardados.");
            return;
        }

        string json = PlayerPrefs.GetString("InventorySave");
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        // 3. Restaurar cada ítem en su slot correspondiente
        foreach (InventoryData invData in data.inventoryData)
        {
            if (invData.slotIndex >= 0 && invData.slotIndex < allSlots.Length)
            {
                // Obtenemos el prefab del diccionario usando tu método
                GameObject itemPrefab = itemDict.GetItemPrefab(invData.itemID);

                if (itemPrefab != null)
                {
                    // Instanciamos el ítem como hijo del slot correcto
                    GameObject newItem = Instantiate(itemPrefab, allSlots[invData.slotIndex].transform);
                    allSlots[invData.slotIndex].currentItem = newItem;
                    
                    // Aseguramos que se centre correctamente en el RectTransform
                    newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
            }
        }

        Debug.Log("¡Inventario cargado y posicionado con éxito!");
    }
}