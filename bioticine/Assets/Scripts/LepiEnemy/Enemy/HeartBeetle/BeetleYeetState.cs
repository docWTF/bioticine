using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleYeetState : BeetleBaseState
{
    public override void EnterState(BeetleStateManager beetleSManager)
    {
        beetleSManager?.YeetEnterState();
    }
    public override void UpdateState(BeetleStateManager beetleSManager, Transform target, Transform player)
    {
        beetleSManager?.TossLogic();
    }
    public override void OnCollision(BeetleStateManager beetleSManager, Collider2D collision)
    {
        beetleSManager?.DestroyAndHealOnCollision(collision);
    }
}
