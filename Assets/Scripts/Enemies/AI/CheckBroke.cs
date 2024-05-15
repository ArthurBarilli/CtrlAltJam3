using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckBroke : ActionNode
{
    public bool boss;   
    protected override void OnStart() {
    }

    protected override void OnStop() {

    }

    protected override State OnUpdate() {
        if(context.enemy.broke == true && boss == true)
        {
            blackboard.bossLight.GetComponent<Light>().enabled = true;
            context.agent.ResetPath();
            return State.Success;
        }
        else if(context.enemy.broke == true && boss == false)
        {
            context.agent.ResetPath();
            return State.Success;
        }
        return State.Failure;
    }
}
