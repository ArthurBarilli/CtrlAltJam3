using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckPlayerDistance : ActionNode
{
    protected override void OnStart() {
        if(blackboard.player == null)
        {
            blackboard.player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (Vector3.Distance(blackboard.player.transform.position , context.transform.position) > 40)
        {
            context.enemy.enemyRenderer.enabled = false;
            return State.Success;
        }
        else if(Vector3.Distance(blackboard.player.transform.position , context.transform.position) < 40 && Vector3.Distance(blackboard.player.transform.position , context.transform.position) > 20)
        {
            context.enemy.enemyRenderer.enabled = true;
            return State.Success;
        }
        else if(Vector3.Distance(blackboard.player.transform.position , context.transform.position) < 20)
        {
            return State.Failure;
        }
        return State.Running;
    }
}
