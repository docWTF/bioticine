using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeeBaseState
{
    public abstract void EnterState(BeeStateManager beeSManager);
    public abstract void UpdateState(BeeStateManager beeSManager, Transform target, Transform player);
    public abstract void OnCollision(BeeStateManager beeSManager, Collider2D collision);
    public abstract void OnCollide(BeeStateManager beeSManager, Collision2D collision);
}
