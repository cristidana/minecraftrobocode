using UnityEngine;

public class EquipItem : MonoBehaviour
{
    public Transform weaponSlot;
    public GameObject currentWeapon;
    public PlayerController pc;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            GameObject newWeapon = other.gameObject;

            Vector3 currentWeaponPosition = currentWeapon.transform.position;
            Quaternion currentWeaponRotation = currentWeapon.transform.rotation;


            currentWeapon.transform.SetParent(null);

            Rigidbody rb = currentWeapon.GetComponent<Rigidbody>();
            Animator anim = currentWeapon.GetComponent<Animator>();

            anim.enabled = false;
            rb.AddForce(transform.up*1f + transform.forward * 2f, ForceMode.Impulse);

            Destroy(other.gameObject);

            currentWeapon = Instantiate(newWeapon, currentWeaponPosition, currentWeaponRotation);

            currentWeapon.transform.SetParent(weaponSlot);

            Animator weaponAnimator = currentWeapon.GetComponent<Animator>();
            weaponAnimator.enabled = true;
            MeshCollider mesh = currentWeapon.GetComponent<MeshCollider>();
            mesh.enabled = false;
            pc.arma = currentWeapon;


        }
    }
}
