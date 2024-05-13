using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckDead : ActionNode
{
    protected override void OnStart() {

    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(context.enemy.dead == true)
        {
            return State.Success;
        }
        return State.Failure;
    }
}
