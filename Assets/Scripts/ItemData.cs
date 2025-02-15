using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class ItemData : MonoBehaviour
{
    public string _name;
    public int id, count;
    [Multiline]
    public string description;
    public bool isUniq;

    public ItemData(string _name,int id,
        int count,string description,bool isUniq)
    {
        this._name = _name;
        this.id = id;
        this.count = count;
        this.description = description;
        this.isUniq = isUniq;
    }

}
