using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class FlyPatrolState : FlyBaseState
{
    private float elapse = 0f;
    private float shootCoolDown;
    public override void EnterState(FlyStateManager flySManager)
    {
        //Debug.Log("Entered the patrol state");
        elapse = 0f;
    }

    public override void UpdateState(FlyStateManager flySManager, Transform target, Transform player)
    {
        shootCoolDown = Random.Range(1f, 10f);
        flySManager?.MoveTowards(target);

        elapse += Time.deltaTime;

        //Debug.Log(elapse);

        if (elapse >= shootCoolDown)
        {
            //Debug.Log("start raycastin");
            flySManager?.RaycastOnPlayer();
        }
    }

    public override void OnCollision(FlyStateManager flySManager, Collider2D collision)
    {
        flySManager.CollisionWithPlayer(collision);
    }
}