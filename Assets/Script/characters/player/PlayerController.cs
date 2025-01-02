using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerController : BaseController
{
    private IState moveState;
    private IState idleState;
    private IState dashState;
    private IState jumpState;

    private void OnEnable()
    {
        comboController.OnComboStepExecuted += OnComboStepExecuted;
        InputManager.instance.OnActionTriggeredDown += HandleActionTriggeredDown;
    }

    private void OnDisable()
    {
        comboController.OnComboStepExecuted -= OnComboStepExecuted;
        InputManager.instance.OnActionTriggeredDown -= HandleActionTriggeredDown;
    }
    protected override void Awake()
    {
        base.Awake();
        moveState = new MoveState(this);
        idleState = new IdleState(this);
        dashState = new DashState(this, moveState);
        jumpState = new JumpState(this, idleState);
        stateMachine.ChangeState(idleState);
    }

    protected override void Update()
    {
        if (GameManager.instance.CurrentGameState != GameState.Playing) return;

        if (comboController != null && comboController.IsComboExecuting()) return;

        base.Update();

        CheckState();
    }

    private void CheckState()
    {
        if(stateMachine.IsCurrentState(dashState)) return;

        if (!characterController.isGrounded) return;

        if (InputManager.instance.directionalLeftInput != Vector2.zero)
        {
            stateMachine.ChangeState(moveState);
        }
        else
        {
            stateMachine.ChangeState(idleState);
        }
    }

    private void OnComboStepExecuted(string obj)
    {
        Debug.Log($"Animation name execute: {obj}");
        animationController.PlayAnimation(obj);
    }

    private void HandleActionTriggeredDown(InputManager.InputAction action)
    {
        if (GameManager.instance.HasPlayerState(PlayerState.Attack))
        {
            switch (action)
            {
                case InputManager.InputAction.ButtonB:
                    stateMachine.ChangeState(dashState);
                    break;

                case InputManager.InputAction.ButtonA:
                    stateMachine.ChangeState(jumpState);
                    break;

                case InputManager.InputAction.ButtonX:
                    comboController.TryExecuteLightCombo();
                    break;

                default:
                    break;
            }
        }
    }
}
