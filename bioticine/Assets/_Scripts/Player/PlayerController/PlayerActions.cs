using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    public GameObject weaponMelee;
    public bool isAttacking;
    public int attackCombo;

    private float resetComboTimer;
    private float resetComboDelay = 1.2f;


    private void Awake()
    {
        EquipWeapon();
    }

    private void Update()
    {
        if (resetComboTimer > 0)
        {
            resetComboTimer -= Time.deltaTime;
            if (resetComboTimer <= 0 && !isAttacking)
            {
                ResetCombo();
            }
        }
    }

    public void EquipWeapon()
    {
        foreach (Transform child in gameObject.transform)
        {
            if (child.CompareTag("WeaponMelee"))
            {
                weaponMelee = child.gameObject; //placeholder solution. for proper implementation maybe find gameobject with weapon class
            }
        }
    }

    public void Attack()
    {
        if (weaponMelee != null && !isAttacking)
        {
            StartCoroutine(AttempToAttack());
        }

    }

    private IEnumerator AttempToAttack()
    {
        isAttacking = true;
        weaponMelee.GetComponent<MeleeWeapon>().SetAttackActive(attackCombo);
        yield return new WaitForSeconds(0.1f);
        weaponMelee.GetComponent<MeleeWeapon>().DamageEnemy();
        yield return new WaitForSeconds(0.3f);
        weaponMelee.GetComponent<MeleeWeapon>().SetAttackInactive(attackCombo);
        weaponMelee.GetComponent<MeleeWeapon>().ClearEnemyList();
        attackCombo += 1;
        isAttacking = false;

        if (attackCombo > 3)
        {
            attackCombo = 0;
        }

        resetComboTimer = resetComboDelay;
    }

    public void ResetCombo()
    {
        attackCombo = 0;
    }

}
