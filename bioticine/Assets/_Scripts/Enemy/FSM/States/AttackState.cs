using FSM;
using System;

namespace LlamAcademy.FSM
{
    public class AttackState : EnemyStateBase
    {
        public AttackState(
            bool needsExitTime,
            Enemy Enemy,
            Action<State<EnemyState, StateEvent>> onEnter,
            float ExitTime = 1f) : base(needsExitTime, Enemy, ExitTime, onEnter) {}

        public override void OnEnter()
        {
            Agent.isStopped = true;
            base.OnEnter();
            Animator.Play("Atk");
        }
    }
}
