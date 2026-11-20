using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventorySaveData
{
    public List<InventoryData> items = new List<InventoryData>();
}

public class SaveButton : MonoBehaviour
{
    [SerializeField] private Button saveButton;
    [SerializeField] private GameObject inventoryWindow; // Ventana o panel principal del inventario

    private string savePath;

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "inventario.json");

        if (saveButton != null)
        {
            saveButton.onClick.AddListener(GuardarInventario);
        }
    }

    public void GuardarInventario()
    {
        if (inventoryController.Instance == null) return;

        InventorySaveData data = new InventorySaveData
        {
            items = inventoryController.Instance.GetInventoryItems()
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Inventario guardado en: " + savePath);

        if (inventoryWindow != null)
        {
            inventoryWindow.SetActive(false);
        }
    }

    public void CargarInventario()
    {
        if (!File.Exists(savePath)) return;

        string json = File.ReadAllText(savePath);
        InventorySaveData data = JsonUtility.FromJson<InventorySaveData>(json);

        if (data != null && inventoryController.Instance != null)
        {
            inventoryController.Instance.SetInventoryItems(data.items);
            Debug.Log("Inventario cargado.");
        }
    }
}