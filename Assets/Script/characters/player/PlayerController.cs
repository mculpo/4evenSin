using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController, IStats
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

    /*
     * -------------------------------------------------
     * STATS
     * -------------------------------------------------
    */

    public void RevertStat(int stat, int baseStat)
    {
        if (stat > baseStat)
        {
            stat = baseStat;
        }
    }

    public void RevertStat(float stat, float baseStat)
    {
        if (stat > baseStat)
        {
            stat = baseStat;
        }
    }

    public void ModifyStat(int stat, int baseStat, int maxStat, int mod, float seconds)
    {
        var nStat = Mathf.Clamp(stat, 1, maxStat);

        if(nStat != stat)
        {
            stat = nStat;
            StartCoroutine(WhileUnderStatMod(stat, baseStat, seconds));
        }
    }

    public void ModifyStat(float stat, float baseStat, float maxStat, float mod, float seconds)
    {
        var nStat = Mathf.Clamp(stat, 1, maxStat);

        if (nStat != stat)
        {
            stat = nStat;
            StartCoroutine(WhileUnderStatMod(stat, baseStat, seconds));
        }
    }

    public void RegenerateStat(int stat, int baseStat, float perSec, float baseSec)
    {
        throw new System.NotImplementedException();
    }

    public void RegenerateStat(float stat, float baseStat, float perSec, float baseSec)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator WhileUnderStatMod(int stat, int baseStat, float seconds)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator WhileUnderStatMod(float stat, float baseStat, float seconds)
    {
        throw new System.NotImplementedException();
    }
}
