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

            Destroy(currentWeapon);
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
