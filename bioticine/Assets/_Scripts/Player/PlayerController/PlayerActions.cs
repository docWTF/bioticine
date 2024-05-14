using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    public GameObject weaponMelee;
    public bool isAttacking;
    public int attackCombo;
    public float attackComboMultiplier;
    public float attackStaminaUsage;
    public float attackSpeed;

    private float resetComboTimer;
    private float resetComboDelay = 1.2f;
    private string attackAnimation;

    public Animator animator;
    public PlayerMovement playerMovement;
    public AnimationManager animationManager;


    private void Awake()
    {
        EquipWeapon();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        animationManager = GetComponent<AnimationManager>();
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
        if (weaponMelee != null && !isAttacking && !playerMovement.isDashing)
        {
            StartCoroutine(AttempToAttack());
        }

    }

    private IEnumerator AttempToAttack()
    {
        if (PlayerStats.Instance.stamina <= 0)
        {
            yield break;
        }

        if (attackCombo == 0 || attackCombo == 1)
        {
            attackComboMultiplier = 1;
            Debug.Log("Combo Multiplier: " + attackComboMultiplier);
        }
        else if (attackCombo == 2)
        {
            attackComboMultiplier = 0.90f;
            Debug.Log("Combo Multiplier: " + attackComboMultiplier);
        }
        else if (attackCombo == 3)
        {
            attackComboMultiplier = 1.2f;
            Debug.Log("Combo Multiplier: " + attackComboMultiplier);
        }

        PlayerStats.Instance.UseStamina(attackStaminaUsage);
        isAttacking = true;
        animator.SetBool("isAttacking", true);
        animator.speed = animationManager.GetAnimationStateSpeed(animationManager.currentStateName) + (animationManager.GetAnimationStateSpeed(animationManager.currentStateName) * PlayerStats.Instance.speedMultiplier);
        weaponMelee.GetComponent<MeleeWeapon>().SetAttackActive(attackCombo);
        
        yield return new WaitForSeconds(0.1f);
        weaponMelee.GetComponent<MeleeWeapon>().DamageEnemy(attackComboMultiplier);
        yield return new WaitForSeconds(attackSpeed - (attackSpeed * PlayerStats.Instance.speedMultiplier));
        weaponMelee.GetComponent<MeleeWeapon>().SetAttackInactive(attackCombo);
        weaponMelee.GetComponent<MeleeWeapon>().ClearEnemyList();
        attackCombo += 1;
        isAttacking = false;
        animator.SetBool("isAttacking", false);
        animator.speed = 1;

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
