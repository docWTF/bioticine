using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeYeetState : BeeBaseState
{
    private float elapse = 0f;
    public override void EnterState(BeeStateManager beeSManager)
    {
        elapse = 0f;
        beeSManager?.YeetEnterState();
    }
    public override void UpdateState(BeeStateManager beeSManager, Transform target, Transform player)
    {
        elapse += Time.deltaTime;
        beeSManager?.TossLogic();
    }
    public override void OnCollision(BeeStateManager beeSManager, Collider2D collision)
    {
        beeSManager.DestroyWhenTouchWall(collision);
    }

    public override void OnCollide(BeeStateManager beeSManager, Collision2D collision)
    {
        beeSManager?.BounceOffWall(collision);
    }
}
