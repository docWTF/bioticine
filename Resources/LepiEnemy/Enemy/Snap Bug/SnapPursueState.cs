using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnapPursueState : SnapBaseState
{
    public override void EnterState(SnapStateManager snapSManager)
    {

    }

    public override void UpdateState(SnapStateManager snapSManager, Transform target, Transform player)
    {

    }

    public override void OnCollision(SnapStateManager snapSManager, Collider2D collision)
    {
        snapSManager.CollisionWithPlayer(collision);
    }
}
