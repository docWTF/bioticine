using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyBaseState
{
    public abstract void EnterState(FlyStateManager flySManager);
    public abstract void UpdateState(FlyStateManager flySManager, Transform target, Transform player);
    public abstract void OnCollision(FlyStateManager flySManager, Collider2D collision);
}
