using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateMachine
{
    private IState currentState;

    public void ChangeState(IState newState)
    {
        if(currentState == newState) return;

        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        currentState?.Execute();
    }

    public bool IsCurrentState(IState state)
    {
        return currentState == state;
    }
}

