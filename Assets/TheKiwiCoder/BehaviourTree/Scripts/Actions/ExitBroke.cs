using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ExitBroke : ActionNode
{
    protected override void OnStart() {
        //exit broke state and recovers
        context.enemy.broke = false;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
