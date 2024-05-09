using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeWallDetect : MonoBehaviour
{
    public GameObject bee;
    private Collider2D beeCollider;
    private BeeStateManager beeStateManagerScript;

    private void Start()
    {
        beeCollider = bee.GetComponent<Collider2D>();
        beeStateManagerScript = bee.GetComponent<BeeStateManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (beeStateManagerScript.tossed == true)
        {
            if (collision.gameObject.CompareTag("BottomWall") || collision.gameObject.CompareTag("TopWall") || collision.gameObject.CompareTag("RightWall") || collision.gameObject.CompareTag("LeftWall"))
            {
                beeCollider.isTrigger = false;
            }
        }
    }
}
