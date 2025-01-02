using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IState
{
    public  readonly PlayerController baseController;

    private IState state;
    public JumpState(PlayerController controller, IState state)
    {
        baseController = controller;
        this.state = state;
    }
    public void Enter()
    {
        Debug.Log("${DashState} -  Enter");
        if (baseController.characterController.isGrounded)
        {
            baseController.velocity.y = Mathf.Sqrt(baseController.jumpHeight * -2f * baseController.gravity);

            baseController.animationController.PlayAnimation("jump", 0.05f);
        }
    }

    public void Execute()
    {
        Debug.Log("${DashState} -  Execute");
        var inputAxis = InputManager.instance.directionalLeftInput;
        Vector3 moveDirection = baseController.cameraTranform.TransformDirection(new Vector3(inputAxis.x, 0, inputAxis.y));
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (inputAxis != Vector2.zero)
        {
            baseController.lastMoveDirection = moveDirection;
            baseController.ApplyMove(moveDirection * baseController.moveSpeed);
        }

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            baseController.transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        if (baseController.characterController.isGrounded)
            baseController.stateMachine.ChangeState(state);

        baseController.ApplyGravity();
    }

    public void Exit()
    {
        Debug.Log("${DashState} -  Exit");
    }
}
