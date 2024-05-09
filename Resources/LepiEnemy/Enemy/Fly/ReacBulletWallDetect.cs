using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReacBulletWallDetect : MonoBehaviour
{
    public Bullet bulletScript;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BottomWall") || collision.gameObject.CompareTag("TopWall") || collision.gameObject.CompareTag("RightWall") || collision.gameObject.CompareTag("LeftWall"))
        {
            bulletScript.collider.isTrigger = false;
            sprite.color = new Color(0.8f, 0f, 0f, 0f);
        }
    }
}
