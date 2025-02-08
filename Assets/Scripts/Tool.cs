using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public ToolTypes type;
    public ToolMaterials material;

    public int damageToEnemy;
    public int damageToBlock;

    void Start()
    {
        damageToEnemy = (int)type * (int)material;

        if(type == ToolTypes.PICKAXE)
        {
            damageToBlock = 4 * (int)material;
        }
        if (type == ToolTypes.SWORD)
        {
            damageToBlock = 1 * (int)material;
        }

    }

    void Update()
    {
        
    }
}
