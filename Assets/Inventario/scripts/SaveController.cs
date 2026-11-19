using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private inventoryController inventoryController;



    void Start()
    {

        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        

        inventoryController = FindAnyObjectByType<inventoryController>();


        LoadGame();
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            inventoryData = inventoryController.GetInventoryItems(),

        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            inventoryController.SetInventoryItems(saveData.inventoryData);
        }
        else
        {
            SaveGame();
        }
    }
}
