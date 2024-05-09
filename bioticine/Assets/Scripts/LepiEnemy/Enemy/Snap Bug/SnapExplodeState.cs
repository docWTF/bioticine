using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
public class SnapExplodeState : SnapBaseState
{
    public override void EnterState(SnapStateManager snapSManager)
    {
        snapSManager?.StartExplode();
    }
    public override void UpdateState(SnapStateManager snapSManager, Transform target, Transform player)
    {
        snapSManager?.MoveTowards(player);
        snapSManager?.RotateTowards(player);   
    }

    public override void OnCollision(SnapStateManager snapSManager, Collider2D collision)
    {
        snapSManager.CollisionWithPlayer(collision);
    }
} 
