using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class GoToMiddleBoss : ActionNode
{
    Vector3 destination;
    protected override void OnStart() {
        destination = blackboard.bossLight.GetComponent<BossFightSetup>().fightCenter.position;
        context.agent.SetDestination(destination);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(Vector3.Distance(context.transform.position, destination) > 2)
        {
            return State.Running;
        }
        else
        {
            return State.Success;
        }
        
    }
}
