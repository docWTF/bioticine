using UnityEngine;
using BehaviorTree;

public class TaskGoToTarget : Node
{
    private Transform _transform;

    public TaskGoToTarget(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (target != null && Vector3.Distance(_transform.position, target.position) > 0.01f)
        {
            // Move the boss towards the target
            _transform.position = Vector3.MoveTowards(
                _transform.position, target.position, BossBT.speed * Time.deltaTime);

            // Lock rotation to only 0 or 180 degrees based on the target's relative position
            if (target.position.x < _transform.position.x)
            {
                // Target is to the left, face left (180 degrees)
                _transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                // Target is to the right, face right (0 degrees)
                _transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        state = NodeState.RUNNING;
        return state;
    }
}
