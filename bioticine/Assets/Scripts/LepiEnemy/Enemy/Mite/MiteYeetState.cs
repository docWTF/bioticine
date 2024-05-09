using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiteYeetState : MiteBaseState
{
    public override void EnterState(MiteStateManager miteSManager)
    {
        miteSManager?.YeetEnterState();
    }

    public override void UpdateState(MiteStateManager miteSManager, Transform target, Transform player)
    {
        miteSManager?.TossLogic();
    }

    public override void OnCollision(MiteStateManager miteSManager, Collider2D collision)
    {
        miteSManager.DestroyAndTeleportWhenTouchWall(collision);
    }
}
