using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class SnapBaseState
{
    public abstract void EnterState(SnapStateManager snapSManager);
    public abstract void UpdateState(SnapStateManager snapSManager, Transform target, Transform player);
    public abstract void OnCollision(SnapStateManager snapSManager, Collider2D collision);
}
