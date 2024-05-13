using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class PlayAnimation : ActionNode
{
    [SerializeField] string triggerName;
    [SerializeField] bool isTrigger;
    [SerializeField] bool boolOnOff;
    protected override void OnStart() {
        if(isTrigger)
        {
            context.animator.SetTrigger(triggerName);
        }
        else 
        {
            context.animator.SetBool(triggerName,boolOnOff);
        }
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
