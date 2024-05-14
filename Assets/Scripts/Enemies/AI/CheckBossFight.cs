using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckBossFight : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.enemy.bossFight)
        {
            if(blackboard.bossLight == null)
            {
                blackboard.bossLight = GameObject.FindGameObjectWithTag("BossLight");
            }
            return State.Failure;
        }
        else
        return State.Success;
    }
}
