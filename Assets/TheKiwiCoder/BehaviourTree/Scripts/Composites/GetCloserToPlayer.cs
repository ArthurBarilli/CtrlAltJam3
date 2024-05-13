using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using Unity.VisualScripting;

public class GetCloserToPlayer : ActionNode
{  
    [SerializeField] Vector3 direction;
    [SerializeField] float speed;
    
    
    protected override void OnStart() {

    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        //checks if there is a player on the blackboard, if so, returns failure so the next sequencer can get the player
        if(blackboard.player == null)
        {
            return State.Failure;
        }

        if(Vector3.Distance(blackboard.player.transform.position, context.transform.position) < 7)
        {
            blackboard.playerComparativeStatus = "Too Close";
            direction = (context.transform.position - blackboard.player.transform.position).normalized;
            context.navMeshAgent.Move(direction * speed);
            context.transform.LookAt(blackboard.player.transform.position);
            return State.Running;
        }
        else if(Vector3.Distance(blackboard.player.transform.position, context.transform.position) > 9)
        {
            blackboard.playerComparativeStatus = "Too Far";
            direction = ( blackboard.player.transform.position - context.transform.position).normalized;
            context.navMeshAgent.Move(direction * speed);
            context.transform.LookAt(blackboard.player.transform.position);
            return State.Running;
        }
        return State.Failure;
    }
}
