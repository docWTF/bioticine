using UnityEngine;
using BehaviorTree;

public class CheckEnemyInAttackRange : Node
{
    private Transform _transform;
    private Animator _animator;
    private float attackRange = BossBT.attackRange;  // Make sure this is accessible

    public CheckEnemyInAttackRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;  // Target is null, no enemy to check
        }

        float distance = Vector3.Distance(_transform.position, target.position);
        if (distance <= attackRange)
        {
            _animator.SetBool("Attacking", true);  // Assuming there is such a parameter in Animator
            _animator.SetBool("Walking", false);
            state = NodeState.SUCCESS;
        }
        else
        {
            _animator.SetBool("Attacking", false);
            _animator.SetBool("Walking", true);
            state = NodeState.FAILURE;
        }

        // Debugging output
        Debug.Log($"CheckEnemyInAttackRange: Target at distance {distance}, State: {state}");

        return state;
    }
}
