using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteController : BaseController
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
    }

    protected override void Update()
    {
        base.Update();

        CheckState();
    }

    private void CheckState()
    {
        if(StateMachine.IsCurrentState(dashState)) return;

        if (!characterController.isGrounded) return;

        if (InputManager.instance.directionalLeftInput != Vector2.zero)
        {
            StateMachine.ChangeState(moveState);
        }
        else
        {
            StateMachine.ChangeState(idleState);
        }
    }

    private void OnComboStepExecuted(string obj)
    {
        Debug.Log($"Animation name execute: {obj}");
        animationController.PlayAnimation(obj);
    }

    private void HandleActionTriggeredDown(InputManager.InputAction action)
    {
        switch (action)
        {
            case InputManager.InputAction.ButtonB:
                StateMachine.ChangeState(dashState);
                break;

            case InputManager.InputAction.ButtonA:
                StateMachine.ChangeState(jumpState);
                break;

            /*case InputManager.InputAction.ButtonX:
                comboController.TryExecuteLightCombo();
                break;*/

            default:
                break;
        }
    }
}
