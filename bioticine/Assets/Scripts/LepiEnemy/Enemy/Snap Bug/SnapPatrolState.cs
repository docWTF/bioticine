using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class SnapPatrolState : SnapBaseState
{
    public override void EnterState(SnapStateManager snapSManager)
    {

    }

    public override void UpdateState(SnapStateManager snapSManager, Transform target, Transform player)
    {
        snapSManager?.MoveTowards(target);
        snapSManager?.RaycastOnPlayer();
    }

    public override void OnCollision(SnapStateManager snapSManager, Collider2D collision)
    {
        snapSManager.CollisionWithPlayer(collision);
    }
}
