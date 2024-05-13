using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class GetPlayer : ActionNode
{
    protected override void OnStart() {
        blackboard.player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
