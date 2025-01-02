using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState
{
    private readonly BaseController enemyController;

    private float attackCooldown = 0.5f;
    private float lastAttackTime = 0f;

    public EnemyAttackState(BaseController controller) { enemyController = controller; }
    public void Enter()
    {
        Debug.Log("${EnemyAttackState} -  Enter");
    }

    public void Execute()
    {
        Debug.Log("${EnemyAttackState} -  Execute");
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    public void Exit()
    {
        Debug.Log("${EnemyAttackState} -  Exit");
    }

    private void Attack()
    {
        Debug.Log("Enemy attacks the target!");
        enemyController.comboController.TryExecuteLightCombo();
    }
}