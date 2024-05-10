using UnityEngine;
using BehaviorTree;
using System.Collections;

public class TaskAttack : Node
{
    private Animator _animator;
    private Transform _lastTarget;
    private EnemyAttack _enemyAttack;
    private MonoBehaviour _monoBehaviour;
    private Coroutine _currentAttackRoutine;
    private float _attackCooldown = 1.0f; // Cooldown time between attacks
    private float _lastAttackTime;

    public TaskAttack(Transform transform, MonoBehaviour monoBehaviour)
    {
        _animator = transform.GetComponent<Animator>();
        _enemyAttack = transform.GetComponentInChildren<EnemyAttack>();
        _monoBehaviour = monoBehaviour;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        // Check if enough time has passed since the last attack
        if (Time.time - _lastAttackTime < _attackCooldown)
        {
            return NodeState.RUNNING; // Still cooling down
        }

        if (target != null && _lastTarget != target)
        {
            _lastTarget = target;
            if (_currentAttackRoutine != null)
            {
                _monoBehaviour.StopCoroutine(_currentAttackRoutine);
            }
            _currentAttackRoutine = _monoBehaviour.StartCoroutine(AttackRoutine());
        }
        else if (target == _lastTarget)
        {
            // Restart attack if the target hasn't changed and cooldown has passed
            _currentAttackRoutine = _monoBehaviour.StartCoroutine(AttackRoutine());
        }

        state = NodeState.RUNNING;
        return state;
    }

    private IEnumerator AttackRoutine()
    {
        _lastAttackTime = Time.time;
        _enemyAttack.SetAttackActive();
        yield return new WaitForSeconds(.3f); // Synchronize with the attack animation start
        _enemyAttack.DamageTarget(); // Apply damage
        yield return new WaitForSeconds(.2f); // Allow some time post-attack
        _enemyAttack.SetAttackInactive();
        _enemyAttack.ClearTargetFromList();
    }
}
