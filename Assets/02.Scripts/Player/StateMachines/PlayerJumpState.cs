using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(_stateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void Exit() 
    {
        base.Exit();

        StopAnimation(_stateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // �����Ҷ��� ���� ũ�� ���ٰ� ������ ������ 0���� ���� ������ ���������ֱ� ������
        // ChageState�� ���ش�.
        //if(_stateMachine.Player.Controller.velocity.y <= 0)
        //{
        //    _stateMachine.ChangeState(_stateMachine.FallState);
        //    return;
        //}

    }
}
