using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public readonly PlayerController baseController;
    public IdleState (PlayerController controller) {  baseController = controller; }
    public void Enter()
    {
        baseController.animationController.PlayAnimation("idle", 0f);
    }

    public void Execute()
    {
        baseController.ApplyGravity();
        Debug.Log("${IdleState} -  Execute");
    }

    public void Exit()
    {
        Debug.Log("${IdleState} -  Exit");
    }
}
