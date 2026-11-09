using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private inventoryController inventoryController;
    private HotbarController hotbarController;

    // Start is called before the first frame update
    void Start()
    {
        // Define save location
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        
        // ¡Faltaba inicializar esta línea!
        inventoryController = FindAnyObjectByType<inventoryController>();
        hotbarController = FindAnyObjectByType<HotbarController>();

        LoadGame();
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            inventoryData = inventoryController.GetInventoryItems(),
            hotbarData = hotbarController.GetHotbarItems()
        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            inventoryController.SetInventoryItems(saveData.inventoryData);
            hotbarController.SetHotbarItems(saveData.hotbarData);
        }
        else
        {
            SaveGame();
        }
    }
}
