using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private int visibility = 30;
    public Transform player;
    private bool isVisible;
    private Vector3 cPos;

    void Start()
    {
        player = GameObject.Find("Player").transform;

        cPos = transform.position;
        isVisible = true;
    }

    void SetActivity(bool isActive)
    {
        int bc = transform.childCount;
        for(int i=0; i < bc; i++)
        {
            transform.GetChild(i).
                gameObject.SetActive(isActive);
        }
        isVisible = isActive;
    }

    void Update()
    {
        float d = Vector3.Distance(cPos,
            new Vector3(
                player.position.x,
                0f,
                player.position.z));

        if(d > visibility && isVisible)
        {
            SetActivity(false);
        }
        if (d < visibility && !isVisible)
        {
            SetActivity(true);
        }
    }
}
