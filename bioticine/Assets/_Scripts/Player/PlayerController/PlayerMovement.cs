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
    public bool isDashing;
    public LayerMask groundLayer; //assign in inspector
    public SpriteRenderer spriteRenderer; //assign in inspector

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }


    public void ProcessMove(Vector2 input)
    {
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

        rigidBody.velocity = new Vector3(input.x, 0, input.y) * speed;

        if (input.x != 0 && input.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (input.x != 0 && input.x > 0)
        {
            spriteRenderer.flipX = true;
        }

    }

    public void Dash(Vector2 input)
    {
        if (input.x != 0 && !isDashing)
        {
            StartCoroutine(AttemptToDash());
            Debug.Log("Attempt to Dash");
        }

    }

    private IEnumerator AttemptToDash()
    {
        isDashing = true;
        float originalSpeed = speed; 
        speed = dashSpeed; 
        Debug.Log("Dashed");

        yield return new WaitForSeconds(iFrameSeconds);

        speed = originalSpeed; 
        isDashing = false; 
    }

}
