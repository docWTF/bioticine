using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeePatrolState : BeeBaseState
{
    private float elapse = 0f;
    private float shootCoolDown;
    public override void EnterState(BeeStateManager beeSManager)
    {
        elapse = 0f;
    }
    public override void UpdateState(BeeStateManager beeSManager, Transform target, Transform player)
    {
        shootCoolDown = Random.Range(1f, 8f);
        beeSManager?.MoveTowards(target);

        elapse += Time.deltaTime;

        //Debug.Log(elapse);

        if (elapse >= shootCoolDown)
        {
            //Debug.Log("start raycastin");
            beeSManager?.RaycastOnPlayer();
        }
    }
    public override void OnCollision(BeeStateManager beeSManager, Collider2D collision)
    {
        beeSManager.CollisionWithPlayer(collision);
    }

    public override void OnCollide(BeeStateManager beeSManager, Collision2D collision)
    {

    }
}
