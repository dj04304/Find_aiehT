using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool _alreadyAppliedForce; // ���� ���� �޴��� 
    private bool _alreadyApplyCombo; // �޺��� �����ߴ���

    AttackInfoData _attackInfoData;

    public PlayerComboAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Player.AnimationData.ComboAttackParameterHash);

        _alreadyApplyCombo= false;
        _alreadyAppliedForce = false;

        int comboIndex = _stateMachine.ComboIndex;
        _attackInfoData = _stateMachine.Player.Data.AttackData.GetAttackInfo(comboIndex);
        _stateMachine.Player.Animator.SetInteger("Combo", comboIndex);

    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Player.AnimationData.ComboAttackParameterHash);

        if (!_alreadyApplyCombo)
            _stateMachine.ComboIndex = 0;
    }

    private void TryComboAttack()
    {
        if (_alreadyApplyCombo) return; // �޺��� �̹� �� ��쿡 alreadyApplyCombo�� true�̱� ������ return

        if (_attackInfoData.ComboStateIndex == -1) return; // ������ �����̱� ������ return

        if (!_stateMachine.IsAttacking) return; // �������� �ƴϱ� ������ return

        _alreadyApplyCombo = true;
    }

    private void TryApplyForce()
    {
        if (_alreadyAppliedForce) return;
        _alreadyAppliedForce = true;

        _stateMachine.Player.ForceReceiver.Reset();

        float comboForceMultiplier = 1.0f + (_stateMachine.ComboIndex * 0.1f); 
        float scaledForce = _attackInfoData.Force * comboForceMultiplier;

        _stateMachine.Player.ForceReceiver.AddForce(_stateMachine.Player.transform.forward * scaledForce);
    }


    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(_stateMachine.Player.Animator, "Attack");
           
        if(normalizedTime < 1f) // �ִϸ��̼��� ������
        {
            if (normalizedTime >= _attackInfoData.ForceTransitionTime)
                TryApplyForce();

            if (normalizedTime >= _attackInfoData.ComboTransitionTime)
                TryComboAttack();

        }
        else
        {

            if(_alreadyApplyCombo)
            {
                _stateMachine.ComboIndex = _attackInfoData.ComboStateIndex;
                _stateMachine.ChangeState(_stateMachine.ComboAttackState);
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
            }

        }
    }

}
