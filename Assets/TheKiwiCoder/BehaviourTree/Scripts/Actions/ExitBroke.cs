using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEditor;

public class ExitBroke : ActionNode
{
    protected override void OnStart() {
        context.enemy.broke = false;
        context.enemy.GetComponent<BossLeoAi>().RegenerateArmor();
        //exit broke state and recovers
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}