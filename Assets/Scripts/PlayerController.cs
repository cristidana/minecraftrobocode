using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float gravity = 9.8f,
                  rotationS = 90f,
                  speed = 5f,
                  jumpForce = 10f;

    private float verticalSpeed = 0f,
                  mouseX,
                  mouseY,
                  angleX;
    public CharacterController cc;
    public Camera cam;


    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    void Update()
    {
        RotateCharacter();
        MoveCharacter();
    }

    void RotateCharacter()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(
            new Vector3(0f, mouseX * rotationS * Time.deltaTime,0f
            ));

        angleX += mouseY * rotationS * Time.deltaTime * -1;

        angleX = Mathf.Clamp(angleX, -60f, 60f);

        cam.transform.localEulerAngles = new Vector3(
                                         angleX,0f,0f);
    }

    void MoveCharacter()
    {
        Vector3 vel = new Vector3(
                          Input.GetAxis("Horizontal"),
                          0f,
                          Input.GetAxis("Vertical")
                                );
        vel = transform.TransformDirection(vel) * speed;

        if (cc.isGrounded)
        {
            verticalSpeed = 0f;
            if (Input.GetButton("Jump"))
            {
                verticalSpeed = jumpForce;
            }
        }

        verticalSpeed -= gravity * Time.deltaTime;
        vel.y = verticalSpeed;

        cc.Move(vel * Time.deltaTime);

    }
}
