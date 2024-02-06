using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpreadSkillState : PlayerSkillState
{

    SkillInfoData _skillData;


    public PlayerSpreadSkillState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    public override void Enter()
    {
        float normalizedTime = GetNormalizedTime(_stateMachine.Player.Animator, "Skill");

        int _skillIndex = 1;
       
        _skillData = _stateMachine.Player.Data.SkillData.GetSkillData(_skillIndex);


        int _skillCost = _skillData.GetSkillCost();
        _stateMachine.IsSkillCoolTime = _stateMachine.Player.SecondSkillCoolTimeController.IsCoolTime;

        base.Enter();

        StartAnimation(_stateMachine.Player.AnimationData.SpreadSkillParameterHash);

        if (_stateMachine.Player.StaminaSystem.CanUseSkill(_skillCost) && !_stateMachine.Player.SecondSkillCoolTimeController.IsCoolTime)
        {
            if(normalizedTime < 1f)
            {
                _stateMachine.MovementSpeedModifier = 0; // 공격할 때 안움직임
                _stateMachine.Player.SandSkill.EnableCollider();
                _stateMachine.Player.SkillParticle.PlayParticle();

                float skillDamage = _skillData.GetSkillDamage();
                float playerDamage = _stateMachine.Player.Data.PlayerData.GetPlayerAtk();

                float totalDamage = skillDamage + playerDamage;

                _stateMachine.Player.SandSkill.SetAttack(totalDamage);

                _stateMachine.Player.StaminaSystem.UseSkill(_skillCost);

                _stateMachine.Player.SecondSkillCoolTimeController.StartCoolTime(_skillData.GetSkillCoolTime());
            }
            else
            {
                _stateMachine.Player.SandSkill.DisableCollider();
            }

        }
        else
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }

    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(_stateMachine.Player.AnimationData.SpreadSkillParameterHash);

        _stateMachine.Player.SandSkill.DisableCollider();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!_stateMachine.Player.SecondSkillCoolTimeController.IsCoolTime)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return;
        }

        float normalizedTime = GetNormalizedTime(_stateMachine.Player.Animator, "Skill");

        if (normalizedTime >= 1f)
        {
            _stateMachine.Player.SandSkill.DisableCollider();
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }

    }


}
