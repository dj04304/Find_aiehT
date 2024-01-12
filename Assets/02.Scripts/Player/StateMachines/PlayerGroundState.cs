using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // �Է�Ű�� ������ ���� �����̴�. ground���� ���ִ� ������ ground�� �ƴ� �ٸ� state�� ���� ���� Ű�Է��� ���� ���� �� �ٸ� ������ �ؾ��ϱ� ����
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if (_stateMachine.MovementInput == Vector2.zero)
        {
            return;
        }

        _stateMachine.ChangeState(_stateMachine.IdleState);

        base.OnMovementCanceled(context);
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        _stateMachine.ChangeState(_stateMachine.JumpState);
    }

    protected virtual void OnMove()
    {
        _stateMachine.ChangeState(_stateMachine.WalkState);
    }

}
