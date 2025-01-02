using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState :IState
{
    private readonly PlayerController baseController;

    public MoveState (PlayerController controller)
    {
        this.baseController = controller;
    }

    public void Enter()
    {
        Debug.Log("${MoveState} -  Enter");
        baseController.animationController.PlayAnimation("run", 0.0f);
    }

    public void Execute()
    {
        baseController.animationController.PlayAnimation("run", 0.0f);
        Debug.Log("${MoveState} -  Execute");
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

        baseController.ApplyGravity();
    }

    public void Exit()
    {
        Debug.Log("${MoveState} -  Exit");
    }
}
