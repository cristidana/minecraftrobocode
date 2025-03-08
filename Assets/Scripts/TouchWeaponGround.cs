using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchWeaponGround : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
          rb.isKinematic = true; 
            
        }
    }
}
