using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemdictionary : MonoBehaviour
{
    public List <item> itemPrefabs;
    private Dictionary <int,GameObject> itemDictionary;
    private GameObject prefab;

    public itemdictionary()
    {
    }

    private void Awake()
    {
        itemDictionary= new Dictionary<int,GameObject>();

        for (int i=0;i<itemPrefabs.Count; i++)
        {
            if(itemPrefabs[i]!=null)
            {
                itemPrefabs[i].ID= i+1;
            }
        }
        foreach(item item in itemPrefabs)
        {
            itemDictionary[item.ID]=item.gameObject;
        }
    }

    public GameObject GetItemPrefab(int itemID)
    {
        itemDictionary.TryGetValue(itemID,out GameObject prefabs);
        if(prefabs==null)
        {
            Debug.LogWarning($"Item with ID{itemID}not found in dictionary");
        }
        return prefabs;
    }

}
