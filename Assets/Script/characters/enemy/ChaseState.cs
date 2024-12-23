using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    public readonly EnemyController enemyController;
    public ChaseState(EnemyController controller) { enemyController = controller; }
    public void Enter()
    {
        Debug.Log("${ChaseState} -  Enter");
        enemyController.animationController.PlayAnimation("run", 0.1f);
    }

    public void Execute()
    {
        Debug.Log("ChaseState - Execute");
        if (enemyController.target == null) return;

        Vector3 direction = (enemyController.target.position - enemyController.transform.position).normalized;

        // Rotate only on the Y-axis towards the target
        Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        enemyController.transform.rotation = Quaternion.Slerp(
            enemyController.transform.rotation,
            lookRotation,
            Time.deltaTime * 5f // Adjust rotation speed as needed
        );

        enemyController.characterController.Move(direction * enemyController.chaseSpeed * Time.deltaTime);

        enemyController.ApplyGravity();
    }

    public void Exit()
    {
        Debug.Log("${ChaseState} -  Exit");
    }

}
