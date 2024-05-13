using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEditor.ShaderGraph.Internal;

public class AttackRanged : ActionNode
{
    public float projectileSpeed;
    protected override void OnStart() {
        //gets player direction
        Vector3 direction = blackboard.player.transform.position - context.transform.position;
        //shoot
        GameObject currentProjectile = PoolingProjectilesManager.Instance.ThrowProjectileEnemySound();
        currentProjectile.transform.position = context.enemy.projectilePlace.position;
        currentProjectile.GetComponent<Rigidbody>().velocity = direction.normalized * projectileSpeed;
        currentProjectile.GetComponent<EnemyProjectile>().ActivateProjectile();
        currentProjectile.transform.LookAt(blackboard.player.transform.position); 
        Debug.Log("pew pew");
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
