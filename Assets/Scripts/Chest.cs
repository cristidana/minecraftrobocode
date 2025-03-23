using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<ItemData> chestItems = new List<ItemData>();

    private void Awake()
    {
        InventoryManager im
    = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();

        int chestC = Random.Range(3, 8);

        for(int i=0; i< chestC; i++)
        {
            im.CreateItem(
                Random.Range(0, im.items.Length),
                chestItems
                );
        }
    }
}
