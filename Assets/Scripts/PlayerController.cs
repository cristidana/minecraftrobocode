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

    public GameObject arma;
    public float hitSpeed = 15f;
    public float hitLastTime = 0f;


    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    void Update()
    {
        RotateCharacter();
        MoveCharacter();


        RaycastHit hit;

        if(Physics.Raycast
            (cam.transform.position,
            cam.transform.forward,
            out hit,
            5f
            ))
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                ObjectInteraction(
                    hit.transform.gameObject);
            }
        }


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


    void Mine(Block block)
    {
        if(Time.time - hitLastTime > 1 / hitSpeed)
        {
            arma.GetComponent<Animator>()
                .SetTrigger("attack");

            hitLastTime = Time.time;

            block.health -= arma.GetComponent < Tool >()
                                    .damageToBlock;

            if(block.health <= 0)
            {
                block.DestroyBlock();
            }
        }
    }



    void ObjectInteraction(GameObject obj)
    {
        if (obj.tag == "Block") {
            Mine(obj.GetComponent<Block>());
        }
    }


}
