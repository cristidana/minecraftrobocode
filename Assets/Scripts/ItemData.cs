using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string itemName;
    public int count, id;
    public bool isUniq;

    [Multiline]
    public string description;

    public ItemData(string itemName, int count, int id, bool isUniq, string desc)
    {
        this.itemName = itemName;
        this.count = count;
        this.id = id;
        this.isUniq = isUniq;
        this.description = desc;
    }
}