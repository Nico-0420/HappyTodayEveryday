using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarController : MonoBehaviour
{
    public GameObject hotbarPanel;
    public GameObject slotPrefab;
    public int slotCount=10;

    private itemdictionary itemdictionary;
    private Key[] hotbarkeys;

    private void Awake()
    {
        itemdictionary=FindAnyObjectByType<itemdictionary>();

        hotbarkeys=new Key[slotCount];
        for(int i=0; i<slotCount;i++)
        {
            hotbarkeys[i]= i<9? (Key)((int)Key.Digit1 + i): Key.Digit0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        for (int i=0; i<slotCount; i++)
        {
            if(Keyboard.current[hotbarkeys[i]].wasPressedThisFrame)
            {
                UseItemInSlot(i);
            }
        }
    }
    void UseItemInSlot(int index)
    {
        slot slot=hotbarPanel.transform.GetChild(index).GetComponent<slot>();
        if(slot.currentItem !=null)
        {
            item item= slot.currentItem.GetComponent<item>();
            item.UseItem();
        }
    }

        public List< InventoryData>GetHotbarItems()
    {
        List<InventoryData>hotbarData= new List<InventoryData>();
        foreach(Transform slotTransform in hotbarPanel.transform)
        {
            slot slot= slotTransform.GetComponent<slot>();
            if(slot.currentItem!=null)
            {
                item item=slot.currentItem.GetComponent<item>();
                hotbarData.Add(new InventoryData{itemID=item.ID,slotIndex=slotTransform.GetSiblingIndex()});
            }
        }
        return hotbarData;
    }
    public void SetHotbarItems(List<InventoryData> hotbarData)
    {
        foreach(Transform child in hotbarPanel.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i=0; i<slotCount; i++)
        {
            Instantiate(slotPrefab, hotbarPanel.transform);
        }
        foreach(InventoryData data in hotbarData)
        {
            if (data.slotIndex<slotCount)
            {
                slot slot= hotbarPanel.transform.GetChild(data.slotIndex).GetComponent<slot>();
                GameObject itemPrefab= itemdictionary.GetItemPrefab(data.itemID);
                if (itemPrefab!=null)
                {
                    GameObject item= Instantiate(itemPrefab, slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition=Vector2.zero;
                    slot.currentItem= item;
                }
            }
        }
    }
}
