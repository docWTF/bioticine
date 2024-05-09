using UnityEngine;
using UnityEngine.AI;

public class FlyYeetState : FlyBaseState
{
    public override void EnterState(FlyStateManager flySManager)
    {
        flySManager.YeetEnterState();
    }
    public override void UpdateState(FlyStateManager flySManager, Transform target, Transform player)
    {
        flySManager.TossLogic();
    }

    public override void OnCollision(FlyStateManager flySManager, Collider2D collision)
    {
        flySManager?.DestroyWhenTouchWall(collision);
    }
}