using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyShootState : FlyBaseState
{
    public override void EnterState(FlyStateManager flySManager)
    {
        flySManager?.StartShooting();
    }

    public override void UpdateState(FlyStateManager flySManager, Transform target, Transform player)
    {
        flySManager?.RotateTowards(player);
    }
    public override void OnCollision(FlyStateManager flySManager, Collider2D collision)
    {
        flySManager?.CollisionWithPlayer(collision);
    }
}
