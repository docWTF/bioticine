using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MitePatrolState : MiteBaseState
{
    public override void EnterState(MiteStateManager miteSManager)
    {

    }

    public override void UpdateState(MiteStateManager miteSManager, Transform target, Transform player)
    {
        miteSManager?.MoveTowards(target);
    }

    public override void OnCollision(MiteStateManager miteSManager, Collider2D collision)
    {
        miteSManager.CollisionWithPlayer(collision);
    }
}
