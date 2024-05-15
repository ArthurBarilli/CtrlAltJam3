using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ProjectileBoss : ActionNode
{
    [SerializeField]int numberOfProjectiles;
    [SerializeField]int currentProjectiles;
    [SerializeField]float counter;
    [SerializeField]float timeBetweenProjectiles;
    [SerializeField]float projectileSpeed;
    protected override void OnStart() {
        currentProjectiles = 0;
        counter = 0;
    }

    protected override void OnStop() {

    }

    protected override State OnUpdate() {

        if(counter < timeBetweenProjectiles)
        {
            counter+= Time.deltaTime;
        }
        else if(counter >= timeBetweenProjectiles)
        {
            if(currentProjectiles >= numberOfProjectiles)
            {
                return State.Success;
            }
            else
            {
                //throw projectile at player
                //gets player direction
                Vector3 direction = blackboard.player.transform.position - context.transform.position;
                //shoot
                GameObject currentProjectile = PoolingProjectilesManager.Instance.ThrowProjectileBoss();
                currentProjectile.transform.position = context.enemy.projectilePlace.position;
                currentProjectile.GetComponent<Rigidbody>().velocity = direction.normalized * projectileSpeed;
                currentProjectile.GetComponent<EnemyProjectile>().ActivateProjectile();
                currentProjectile.transform.LookAt(blackboard.player.transform.position); 
                currentProjectiles++;
                counter = 0;
            }
        }
        return State.Running;
    }
}
