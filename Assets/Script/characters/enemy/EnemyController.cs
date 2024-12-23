using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    private IState chaseState;
    private IState attackState;

    public Transform target;
    public float attackRange = 1.5f;
    public float chaseSpeed = 2f;

    private void OnEnable()
    {
        comboController.OnComboStepExecuted += OnComboStepExecuted;
    }

    private void OnDisable()
    {
        comboController.OnComboStepExecuted -= OnComboStepExecuted;
    }

    protected override void Awake()
    {
        base.Awake();
        chaseState = new ChaseState(this);
        attackState = new EnemyAttackState(this);
    }

    protected override void Update()
    {
        CheckState();
        base.Update();
    }

    private void OnComboStepExecuted(string obj)
    {
        Debug.Log($"Animation name execute: {obj}");
        animationController.PlayAnimation(obj);
    }

    private void CheckState()
    {
        if (target == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= attackRange)
        {
            stateMachine.ChangeState(attackState);
        }
        else
        {
            if (comboController.IsComboExecuting()) return;
            stateMachine.ChangeState(chaseState);
        }
    }
}
