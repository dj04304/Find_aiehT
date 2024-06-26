using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }
    public override void Enter()
    {
        _stateMachine.Enemy.Agent.speed = 5f;

        base.Enter();
        StartAnimation(_stateMachine.Enemy.AnimationData.GroundParameterHash);
        StartAnimation(_stateMachine.Enemy.AnimationData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Enemy.AnimationData.GroundParameterHash);
        StopAnimation(_stateMachine.Enemy.AnimationData.RunParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (_stateMachine.Enemy.HealthSystem.IsDead)
        {
            _stateMachine.ChangeState(_stateMachine.DieState);
            return;
        }

        _stateMachine.Enemy.Agent.SetDestination(_stateMachine.Target.transform.position);

        if (!IsInChaseRange() && !_stateMachine.Enemy.HealthSystem.Hit)
        {
            _stateMachine.ChangeState(_stateMachine.IdlingState);
            return;
        }
        else if (IsInAttackRange())
        {
            Vector3 targetPosition = new Vector3(_stateMachine.Target.transform.position.x, _stateMachine.Enemy.transform.position.y, _stateMachine.Target.transform.position.z);
            _stateMachine.Enemy.transform.LookAt(targetPosition);
            if (_stateMachine.Enemy.AttackDelay > _stateMachine.Enemy.Data.AttackDelay)
            {
                _stateMachine.Enemy.AttackDelay = 0;
                _stateMachine.ChangeState(_stateMachine.AttackState);
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.IdlingState);
            }
            return;
        }
    }
}