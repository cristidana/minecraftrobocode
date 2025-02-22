using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject slotPref;

    public GameObject inventoryPanel, chestPanel;
    public GameObject invContent, chestContent;

    public ItemData[] items;

    public List<GameObject> invSlots
        = new List<GameObject>();

    public List<GameObject> chestSlots
        = new List<GameObject>();

    void Start()
    {
        inventoryPanel.SetActive(false);
        chestPanel.SetActive(false);

    }
    void CreateItem(int id, List<ItemData> itemList)
    {
        ItemData item = new ItemData(
            items[id].name,
            items[id].id,
            items[id].count,
            items[id].description,
            items[id].isUniq
            );

        if(!item.isUniq && itemList.Count > 0)
        {
            for (int i = 0; i < itemList.Count; i++) {
                if (item.id == itemList[i].id)
                {
                    itemList[i].count += 1;
                    break;
                }
                else if(i == itemList.Count -1)
                {
                    itemList.Add(item);
                    break;
                }


             }
        }
        else if(item.isUniq || (!item.isUniq && itemList.Count == 0))
        {
            itemList.Add(item);
        }
    }

}
