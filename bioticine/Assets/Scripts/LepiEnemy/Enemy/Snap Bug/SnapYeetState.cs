using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SnapYeetState : SnapBaseState
{
    public override void EnterState(SnapStateManager snapSManager)
    {
        snapSManager.YeetEnterState();
    }
    public override void UpdateState(SnapStateManager snapSManager, Transform target, Transform player)
    {
        snapSManager.TossLogic();
    }

    public override void OnCollision(SnapStateManager snapSManager, Collider2D collision)
    {
        snapSManager?.DestroyWhenTouchWall(collision);
    }
}
