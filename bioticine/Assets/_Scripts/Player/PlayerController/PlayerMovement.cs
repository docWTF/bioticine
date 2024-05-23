using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rigidBody;

    public float speed;
    public float dashSpeed;
    public float iFrameSeconds;
    public float groundDist;
    public float dashSpeedCoeffecient;
    public bool isDashing;
    public LayerMask groundLayer; //assign in inspector
    public SpriteRenderer spriteRenderer; //assign in inspector\
    public Animator animator; //placeholder solution

    public MeleeWeapon meleeWeapon;
    public PlayerActions playerActions;

    public float dashStaminaUsage;

    private Vector3 dashDirection;
    
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); //placeholder solution
        meleeWeapon = GetComponentInChildren<MeleeWeapon>();
        playerActions = GetComponentInChildren<PlayerActions>();
    }


    public void ProcessMove(Vector2 input)
    {
        if (PlayerStats.Instance.isSceneStart)
        {
            return;
        }

        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;

        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, groundLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }

        }

        if (playerActions.isAttacking)
        {
            rigidBody.velocity = new Vector3(0, 0, 0);
            return;
        }

        if (isDashing)
        {
            return;
        }

        animator.SetFloat("Speed", input.magnitude);

        rigidBody.velocity = new Vector3(input.x, 0, input.y) * speed;

        if (input.x != 0 && input.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (input.x != 0 && input.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        if (input.x != 0)
        {
            meleeWeapon.transform.rotation = Quaternion.Euler(0, input.x < 0 ? 0 : 180, 0);
        }

    }

    public void Dash(Vector2 input)
    {
        if (playerActions.isAttacking)
        {
            return;
        }

        if ((input.x != 0 || input.y !=0) && !isDashing)
        {
            dashDirection = new Vector3(input.x, 0, input.y).normalized; 
            StartCoroutine(AttemptToDash());
            Debug.Log("Attempt to Dash");
        }

    }

    private IEnumerator AttemptToDash()
    {
        if (PlayerStats.Instance.stamina <= 0)
        {
            yield break;
        }
        PlayerStats.Instance.UseStamina(dashStaminaUsage);
        isDashing = true;
        animator.SetBool("isDashing", true);
        float originalSpeed = speed; 
        speed = dashSpeed + (PlayerStats.Instance.levelStaminaCoeffecient * dashSpeedCoeffecient);
        rigidBody.velocity = dashDirection * speed; 

        yield return new WaitForSeconds(iFrameSeconds);

        speed = originalSpeed; 
        isDashing = false;
        animator.SetBool("isDashing", false);
    }

}
