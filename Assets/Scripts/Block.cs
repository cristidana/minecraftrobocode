using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int health { get; set; }

    public BlockTypes block;

    void Start()
    {
        health = (int)block;


    }

    public void DestroyBlock()
    {
        GameObject mini = Resources.Load<GameObject>(
                            "mini" + block.ToString());

        Instantiate(
            mini,
            transform.position,
            Quaternion.identity
            );

        Destroy(gameObject);
    }
}
