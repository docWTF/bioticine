using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Collider2D collider;
    private Rigidbody2D rb;
    private Vector2 currentVelocity;
    private int currentBounce = 0;
    public int maxBounce = 2;
    public float bulletSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        //Register the current velocity first so the bullet can bounce, if get current velocity when bullet bounce, it touches the wall so it is set to 0, so i have to do this
        currentVelocity = transform.right.normalized * bulletSpeed;
        rb.velocity = currentVelocity;

        //Debug.Log(rb.velocity);
        //Debug.Log(rb.velocity.magnitude);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("BottomWall") || collision.gameObject.CompareTag("TopWall") || collision.gameObject.CompareTag("RightWall") || collision.gameObject.CompareTag("LeftWall"))
        {
            if (currentBounce < maxBounce)
            {
                Vector2 newDirection = Vector2.Reflect(currentVelocity.normalized, collision.contacts[0].normal);
                rb.velocity = newDirection * bulletSpeed;
                currentVelocity = rb.velocity;
                currentBounce++;

                float rotateAngle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
                this.transform.rotation = Quaternion.Euler(0f, 0f, rotateAngle);

                collider.isTrigger = true;
            }

            else
            {
                Destroy(this.gameObject);
            }
        }        
    }
}
