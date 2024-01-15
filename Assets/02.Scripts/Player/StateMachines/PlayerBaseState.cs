using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine _stateMachine;
    protected readonly PlayerGroundData _groundData;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        _stateMachine = playerStateMachine;
        _groundData = _stateMachine.Player.Data.GroundData;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }


    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        Move();
    }
    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = _stateMachine.Player.Input;
        input.PlayerActions.Move.canceled += OnMovementCanceled;
        input.PlayerActions.Run.started += OnRunStarted;

        input.PlayerActions.Jump.started += OnJumpStarted;
        input.PlayerActions.Dash.started += OnDashStarted;

        input.PlayerActions.Attack.performed += OnAttackPerform;
        input.PlayerActions.Attack.canceled+= OnAttackCanceled;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = _stateMachine.Player.Input;
        input.PlayerActions.Move.canceled -= OnMovementCanceled;
        input.PlayerActions.Run.started -= OnRunStarted;

        input.PlayerActions.Jump.started -= OnJumpStarted;
        input.PlayerActions.Dash.started -= OnDashStarted;


        input.PlayerActions.Attack.performed -= OnAttackPerform;
        input.PlayerActions.Attack.canceled -= OnAttackCanceled;
    }

    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnDashStarted(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnAttackPerform(InputAction.CallbackContext context)
    {
        _stateMachine.IsAttacking = true;
    }

    protected virtual void OnAttackCanceled(InputAction.CallbackContext context)
    {
        _stateMachine.IsAttacking = false;
    }

    //
    private void ReadMovementInput()
    {
        _stateMachine.MovementInput = _stateMachine.Player.Input.PlayerActions.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Rotate(movementDirection);

        Move(movementDirection);
    }

    private void Move(Vector3 movementDirection)
    {
        Rigidbody rigidbody = _stateMachine.Player.Rigidbody;

        float movementSpeed = GetMovemenetSpeed();

        movementDirection *= movementSpeed;
        movementDirection.y = rigidbody.velocity.y;

        rigidbody.velocity = movementDirection;

    }

    protected void ForceMove()
    {
        _stateMachine.Player.Rigidbody.AddForce(_stateMachine.Player.ForceReceiver.Movement);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 forward = _stateMachine.MainCameraTransform.forward;
        Vector3 right = _stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * _stateMachine.MovementInput.y + right * _stateMachine.MovementInput.x;
    }

    private float GetMovemenetSpeed()
    {
        float movementSpeed = _stateMachine.MovementSpeed * _stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

    private void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Transform playerTransform = _stateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, _stateMachine.RotationDamping * Time.deltaTime);

            
        }
    }

    protected void StartAnimation(int animationHash)
    {
        _stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        _stateMachine.Player.Animator.SetBool(animationHash, false);
    }


    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0); // ���� �ִϸ��̼ǿ� ���� ����
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0); // ���� �ִϸ��̼ǿ� ���� ����

        if(animator.IsInTransition(0) && nextInfo.IsTag(tag)) // ���� �±װ� �޾ƿ� �±��̰�, ���� �ִϸ��̼��� Ʈ�������� Ÿ�� �ִ���
        {
            return nextInfo.normalizedTime; // ���� �ִϸ��̼������� ������ (�ִϸ��̼Ǹ��� ���̰� �ٸ��� ������ normalized���ִ� ����)
        }
        else if(!animator.IsInTransition(0) && currentInfo.IsTag(tag)) // �ִϸ��̼��� Ʈ�������� Ÿ������ �ʴٸ�
        {
            return currentInfo.normalizedTime; // ���� �ִϸ��̼����� ���ƿ�
        }
        else
        {
            return 0f;
        }

    }

}