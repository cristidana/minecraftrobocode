using UnityEngine;

public class EquipItem : MonoBehaviour
{
    public Transform weaponSlot;
    public GameObject currentWeapon;
    public PlayerController pc;
    public Transform playerTransform; // Reference to player to drop weapon in front

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            GameObject newWeapon = other.gameObject;

            if (currentWeapon != null)
            {
                DropWeapon(); // Drop current weapon before equipping new one
            }

            EquipNewWeapon(newWeapon);
        }
    }

    void DropWeapon()
    {
        if (currentWeapon == null) return;

        currentWeapon.transform.SetParent(null);

        Rigidbody rb = currentWeapon.GetComponent<Rigidbody>();
        Animator anim = currentWeapon.GetComponent<Animator>();

        if (anim != null) anim.enabled = false;

        if (rb != null)
        {
            rb.isKinematic = false; // Enable physics
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Move the weapon slightly in front of the player
            Vector3 dropPosition = playerTransform.position + playerTransform.forward * 1f + Vector3.up * 0.5f;
            currentWeapon.transform.position = dropPosition;

            // Slightly push it forward to look natural
            rb.AddForce(playerTransform.forward * 1f, ForceMode.Impulse);
        }
    }

    void EquipNewWeapon(GameObject newWeapon)
    {
        // Position the new weapon correctly
        newWeapon.transform.SetParent(weaponSlot);
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localRotation = Quaternion.identity;

        // Disable Rigidbody physics
        Rigidbody rb = newWeapon.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        // Enable animations if available
        Animator anim = newWeapon.GetComponent<Animator>();
        if (anim != null) anim.enabled = true;

        // Update references
        currentWeapon = newWeapon;
        pc.arma = currentWeapon;
    }
}
