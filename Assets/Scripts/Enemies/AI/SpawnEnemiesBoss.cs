using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class SpawnEnemiesBoss : ActionNode
{
    [SerializeField] int currentWalkingLocation;
    [SerializeField] Vector3 walkingPosition;
    protected override void OnStart() {
        //turn lights Off
        blackboard.bossLight.GetComponent<Light>().enabled = false;
        blackboard.bossLight.GetComponent<BossFightSetup>().SpawnEnemies();
        //sets the first walking destination
        currentWalkingLocation = 0;
        walkingPosition = blackboard.bossLight.GetComponent<BossFightSetup>().enemiesSpawn[currentWalkingLocation].position;
        context.agent.SetDestination(walkingPosition);

    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(Vector3.Distance(context.transform.position, walkingPosition) < 2)
        {
            if(currentWalkingLocation > blackboard.bossLight.GetComponent<BossFightSetup>().enemiesSpawn.Count - 1)
            {
                currentWalkingLocation = 0;
                walkingPosition = blackboard.bossLight.GetComponent<BossFightSetup>().enemiesSpawn[currentWalkingLocation].position;
            }
            else
            {
                currentWalkingLocation++;
                walkingPosition = blackboard.bossLight.GetComponent<BossFightSetup>().enemiesSpawn[currentWalkingLocation].position;
            }
            context.agent.SetDestination(walkingPosition);
            return State.Running;
        }        
        return State.Running;
    }
}
