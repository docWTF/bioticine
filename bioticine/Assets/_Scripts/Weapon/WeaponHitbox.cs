using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    private MeleeWeapon parentWeapon;

    private void Awake()
    {
        parentWeapon = GetComponentInParent<MeleeWeapon>();  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            parentWeapon.RegisterHit(other.gameObject);  
        }
    }
}
