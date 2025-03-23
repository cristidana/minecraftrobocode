using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedScale = 5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float turnSpeed = 90f;

    [SerializeField] private Camera goCamera;
    [SerializeField] private GameObject particleObj, currentEquipedItem;
    [SerializeField] private GameObject[] equipableItems;
    [HideInInspector]
    public string itemYouCanEquipeName = EQUIP_NOT_SELECTED_TEXT;

    public const string EQUIP_NOT_SELECTED_TEXT = "EquipeNotSelected";

    private float hitScaleSpeed = 15f;
    private float hitLastTime = 0f;

    private const float gravityScale = 9.8f;
    private float verticalSpeed = 0f,
                  mouseX = 0f,
                  mouseY = 0f,
                  currentAngleX = 0f;

    private CharacterController controller;

    private RaycastHit hit;

    private InventoryManager inventoryManager;
    public List<ItemData> invItems, currentChestItems;
    private Transform itemParent;
    private bool canMove = true;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();

        inventoryManager = FindObjectOfType<InventoryManager>();
        itemParent = GameObject.Find("InventoryContent").transform;
        inventoryManager.CreateItem(0, invItems);
        EquipItem("Pickaxe");

    }
    private void Update()
    {
        if (canMove)
        {
            RotateCharacter();
            MoveCharacter();
            if (Physics.Raycast(goCamera.transform.position, goCamera.transform.forward, out hit, 5f))
            {
                if (Input.GetMouseButton(0))
                {
                    ObjectInteraction(hit.transform.gameObject);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    ItemAbility();
                }

            }
        }
        
    }

    private void ObjectInteraction(GameObject tempObj)
    {
        switch (tempObj.tag)
        {
            case "Block":
                {
                    Dig(tempObj.GetComponent<Block>());
                }
                break;
            case "Enemy":
                break;
            case "Chest":
                {
                    currentChestItems = tempObj.GetComponent<Chest>().chestItems;
                    OpenChest();
                }
                break;
        }
    }

    public void ItemAbility()
    {
        switch (currentEquipedItem.name)
        {
            case "Ground":
                CreateBlock();
                break;
            case "Meat":
                break;
            default:
                break;
        }
    }

    private void CreateBlock()
    {
        GameObject blockPref = Resources.Load<GameObject>("Ground");
        Vector3 tempPos = hit.transform.gameObject.transform.position;
        Vector3 newBlockPos = Vector3.zero;//new Vecotr3(0,0,0)
        if (hit.transform.CompareTag("Block"))
        {
            GameObject curBlock = Instantiate(blockPref);
            if (hit.point.y == tempPos.y + 0.5f)
            {
                newBlockPos = new Vector3(tempPos.x, tempPos.y + 1f, tempPos.z);
            }
            else if (hit.point.y == tempPos.y - 0.5f)
            {
                newBlockPos = new Vector3(tempPos.x, tempPos.y - 1f, tempPos.z);
            }
            if (hit.point.z == tempPos.z + 0.5f)
            {
                newBlockPos = new Vector3(tempPos.x, tempPos.y, tempPos.z + 1f);
            }
            else if (hit.point.z == tempPos.z - .5f)
            {
                newBlockPos = new Vector3(tempPos.x, tempPos.y, tempPos.z - 1f);
            }
            if (hit.point.x == tempPos.x + .5f)
            {
                newBlockPos = new Vector3(tempPos.x + 1f, tempPos.y, tempPos.z);
            }
            else if (hit.point.x == tempPos.x - .5f)
            {
                newBlockPos = new Vector3(tempPos.x - 1f, tempPos.y, tempPos.z);
            }
            curBlock.transform.position = newBlockPos;
            ModifyItemCount("Ground");
        }
    }

    private void ModifyItemCount(string itemName)
    {
        foreach (ItemData item in invItems)
        {
            if (item.itemName == itemName)
            {
                item.count--;
                if (item.count <= 0)
                {
                    invItems.Remove(item);
                    EquipItem(invItems[0].itemName);
                }
                break;
            }
        }
    }

    private void RotateCharacter()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(new Vector3(0f, mouseX * turnSpeed * Time.deltaTime, 0f));

        currentAngleX += -mouseY * turnSpeed * Time.deltaTime;
        currentAngleX = Mathf.Clamp(currentAngleX, -60f, 60f);
        goCamera.transform.localEulerAngles = new Vector3(currentAngleX, 0f, 0f);
    }

    private void MoveCharacter()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"),
                                        0f,
                                        Input.GetAxis("Vertical"));
        direction = transform.TransformDirection(direction) * speedScale;
        if (controller.isGrounded)
        {
            verticalSpeed = 0f;
            if (Input.GetButton("Jump"))
            {
                verticalSpeed = jumpForce;
            }

        }
        else
        {
            verticalSpeed -= gravityScale * Time.deltaTime;
        }
        direction.y = verticalSpeed;
        controller.Move(direction * Time.deltaTime);
    }

    private void Dig(Block block)
    {
        if (Time.time - hitLastTime > 1 / hitScaleSpeed)
        {
            currentEquipedItem.GetComponent<Animator>().SetTrigger("attack");
            hitLastTime = Time.time;
            Tool currentToolInfo;
            if (currentEquipedItem.TryGetComponent<Tool>(out currentToolInfo))
            {
                block.Health -= currentToolInfo.damageToBlock;
            }
            else
            {
                block.Health -= 1;
            }

              if (block.Health <= 0)
            {
                block.DestroyBehaviour();
            }
        }
    }

    private void EquipItem(string toolName)
    {
        foreach (GameObject tool in equipableItems)
        {
            if (tool.name == toolName)
            {
                tool.SetActive(true);
                currentEquipedItem = tool;
                toolName = EQUIP_NOT_SELECTED_TEXT;
            }
            else
            {
                tool.SetActive(false);
            }
        }
    }

    public void OpenChest()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        canMove = false;
        if (!inventoryManager.chestPanel.activeSelf)
        {
            inventoryManager.chestPanel.SetActive(true);
            Transform itemParent = GameObject.Find("ChestContent").transform;
            for (int i = 0; i < currentChestItems.Count; i++)
            {
                inventoryManager.InstatiateItem(currentChestItems[i], itemParent, inventoryManager.chestItems);
            }
        }
    }

    
}
