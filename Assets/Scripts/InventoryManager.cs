using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject slotPref;
    [SerializeField] public GameObject inventoryPanel, chestPanel, descriptionPanel;
    [SerializeField] public GameObject inventoryContent, chestContent;
    [SerializeField] public ItemData[] items; // ????? ???? ????????? ???? ItemData ? ???

    public List<GameObject> inventorySlots = new List<GameObject>(); // ?????? ?????????, ?? ?????? ?????????????
    public List<GameObject> chestItems = new List<GameObject>();

    void Start()
    {
        inventoryPanel.SetActive(false);
        chestPanel.SetActive(false);
    }

    public void CreateItem(int itemid, List<ItemData> itemsList)
    {
        ItemData item = new ItemData(
            items[itemid].itemName, // TODO
            items[itemid].count,
            items[itemid].id,
            items[itemid].isUniq,
            items[itemid].description);

        if (!item.isUniq && itemsList.Count > 0)
        {
            for (int i = 0; i < itemsList.Count; i++)
            {
                if (item.id == itemsList[i].id)
                {
                    itemsList[i].count++;
                    break;
                }
                else if (i == itemsList.Count - 1)
                {
                    itemsList.Add(item);
                    break;
                }
            }
        }
        else if (item.isUniq || (!item.isUniq && itemsList.Count == 0))
        {
            itemsList.Add(item);
        }
    }

    public void InstatiateItem(ItemData item, Transform parent, List<GameObject> itemsList)
    {
        GameObject slot = Instantiate(slotPref);
        slot.transform.SetParent(parent.transform);
        slot.AddComponent<Slot>();
        slot.GetComponent<Slot>().itemData = item;
        slot.transform.Find("ItemNameText").GetComponent<Text>().text = item.itemName;
        slot.transform.Find("ItemImg").GetComponent<Image>().sprite = Resources.Load<Sprite>(item.itemName);
        slot.transform.Find("ItemCountText").GetComponent<Text>().text = item.count.ToString();
        slot.transform.Find("ItemCountText").GetComponent<Text>().color = item.isUniq ? Color.clear : Color.white;
        itemsList.Add(slot);
    }
}
