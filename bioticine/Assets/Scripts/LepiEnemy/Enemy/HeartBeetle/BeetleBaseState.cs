using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeetleBaseState
{
    public abstract void EnterState(BeetleStateManager beetleSManager);
    public abstract void UpdateState(BeetleStateManager beetleSManager, Transform target, Transform player);
    public abstract void OnCollision(BeetleStateManager beetleSManager, Collider2D collision);
}
