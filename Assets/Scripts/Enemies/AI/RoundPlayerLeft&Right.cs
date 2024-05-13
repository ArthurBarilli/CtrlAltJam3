using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Windows.Speech;
using Unity.VisualScripting;

public class RoundPlayerLeftRight : ActionNode
{
    [SerializeField] int dir;
    [SerializeField] float walkingTime;
    [SerializeField] float speed;
    [SerializeField] float counter;
    protected override void OnStart() {
        counter = 0;
        dir = Random.Range(1,3);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(dir == 1 && counter < walkingTime)
        {
            counter += Time.deltaTime;
            context.transform.LookAt(blackboard.player.transform.position);
            context.transform.RotateAround(blackboard.player.transform.position, Vector3.up, -speed * Time.deltaTime);
        }
        else if(dir == 2 && counter < walkingTime)
        {
            counter += Time.deltaTime;
            context.transform.LookAt(blackboard.player.transform.position);
            context.transform.RotateAround(blackboard.player.transform.position, Vector3.up, speed * Time.deltaTime);
        }
        else
        {
            Finish();
            return State.Success;
        }
        return State.Running;
    }

    void Finish()
    {
        counter = 0;
    }
}
