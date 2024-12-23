using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : IState
{
    public BaseController baseController;

    private IState state;
    private float dashTimeLeft;
    private Vector3 dashDirection;
    public DashState(BaseController controller, IState state) {
        baseController = controller; 
        this.state = state;
    }
    public void Enter()
    {
        Debug.Log("${DashState} -  Enter");
        if (dashTimeLeft == 0)
        {
            baseController.animationController.PlayAnimation("dash", 0.05f);

            var inputAxis = InputManager.instance.directionalLeftInput;

            dashTimeLeft = baseController.dashTime;

            dashDirection = new Vector3(inputAxis.x, 0, inputAxis.y).normalized;

            dashDirection = inputAxis != Vector2.zero
            ? baseController.lastMoveDirection
            : baseController.transform.forward.normalized;

            Debug.Log($"Dash Direction: {dashDirection}");
        }
    }

    public void Execute()
    {
        Debug.Log("${DashState} -  Execute");
        if (dashTimeLeft > 0)
        { 
            baseController.characterController.Move(dashDirection * (baseController.dashDistance / baseController.dashTime) * Time.deltaTime);

            dashTimeLeft -= Time.deltaTime;

            if (dashTimeLeft <= 0)
            {
                dashTimeLeft = 0;
                baseController.StateMachine.ChangeState(state);
            }
        }
    }

    public void Exit()
    {
        Debug.Log("${DashState} -  Exit");
    }
}
