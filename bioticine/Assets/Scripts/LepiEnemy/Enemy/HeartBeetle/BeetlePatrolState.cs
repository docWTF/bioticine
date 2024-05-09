using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetlePatrolState : BeetleBaseState
{
    public override void EnterState(BeetleStateManager beetleSManager)
    {

    }
    public override void UpdateState(BeetleStateManager beetleSManager, Transform target, Transform player)
    {
        beetleSManager?.MoveTowards(target);
    }
    public override void OnCollision(BeetleStateManager beetleSManager, Collider2D collision)
    {
        beetleSManager?.CollisionWithPlayer(collision);
    }
}
