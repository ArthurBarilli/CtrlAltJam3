using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckPhaseBoss : ActionNode
{
    public float phaseLife;
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(context.enemy.life <= phaseLife)
        {
            return State.Success;
        }
        else
        return State.Failure;
    }
}
