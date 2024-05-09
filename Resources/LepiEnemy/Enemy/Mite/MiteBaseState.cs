using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiteBaseState
{
    public abstract void EnterState(MiteStateManager miteSManager);
    public abstract void UpdateState(MiteStateManager miteSManager, Transform target, Transform player);
    public abstract void OnCollision(MiteStateManager miteSManager, Collider2D collision);

}
