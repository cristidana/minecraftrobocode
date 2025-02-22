using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class ItemData : MonoBehaviour
{
    public string _name;
    public int id, count;
    public bool isUniq;
    [Multiline]
    public string description;

    public ItemData(string _name,int id,
        int count,string description,bool isUniq)
    {
        this._name = _name;
        this.id = id;
        this.count = count;
        this.isUniq = isUniq;
        this.description = description;
    }

}
