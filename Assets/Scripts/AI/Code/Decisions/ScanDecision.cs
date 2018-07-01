﻿using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Scan")]
public class ScanDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool noEnemyInSight = Scan(controller);
        return noEnemyInSight;
    }

    private bool Scan(StateController controller)
    {
        controller.navMeshAgent.destination = controller.targetLastKnownPosition;
        controller.navMeshAgent.stoppingDistance = controller.enemyStats.waypointStoppingDistance;

        if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance)
        {
            controller.navMeshAgent.isStopped = true;
            controller.transform.Rotate(0, controller.enemyStats.searchingTurnSpeed * Time.deltaTime, 0);
            return controller.CheckIfCountDownElapsed(controller.enemyStats.searchDuration);
        }

        return false;
    }
}