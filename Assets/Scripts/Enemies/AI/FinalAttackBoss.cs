using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using Unity.VisualScripting;

public class FinalAttackBoss : ActionNode
{
    [SerializeField] int currentWalkingLocation;
    [SerializeField] Vector3 walkingPosition;
    [SerializeField]int numberOfProjectiles;
    [SerializeField]int currentProjectiles;
    [SerializeField]float counter;
    [SerializeField]float timeBetweenProjectiles;
    [SerializeField]float projectileSpeed;
    Vector3 direction;
    protected override void OnStart() {
        //turn lights Off
        blackboard.bossLight.GetComponent<Light>().enabled = false;
        blackboard.bossLight.GetComponent<BossFightSetup>().SpawnEnemies();
        //sets the first walking destination
        currentWalkingLocation = 0;
        walkingPosition = blackboard.bossLight.GetComponent<BossFightSetup>().enemiesSpawn[currentWalkingLocation].position;
        context.agent.SetDestination(walkingPosition);

        currentProjectiles = 0;
        counter = 0;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(Vector3.Distance(context.transform.position, walkingPosition) < 2)
        {
           
            //throw projectile at player
            //gets player direction
            direction = blackboard.player.transform.position - context.transform.position;
            //shoot
            GameObject currentProjectile = PoolingProjectilesManager.Instance.ThrowProjectileBoss();
            currentProjectile.transform.position = context.enemy.projectilePlace.position;
            currentProjectile.GetComponent<Rigidbody>().velocity = direction.normalized * projectileSpeed;
            currentProjectile.GetComponent<EnemyProjectile>().ActivateProjectile();
            currentProjectile.transform.LookAt(blackboard.player.transform.position); 
            currentProjectiles++;
                
            if(currentWalkingLocation > blackboard.bossLight.GetComponent<BossFightSetup>().enemiesSpawn.Count - 1)
            {
                currentWalkingLocation = 0;
                walkingPosition = blackboard.bossLight.GetComponent<BossFightSetup>().enemiesSpawn[currentWalkingLocation].position;
            }
            else
            {
                currentWalkingLocation++;
                walkingPosition = blackboard.bossLight.GetComponent<BossFightSetup>().enemiesSpawn[currentWalkingLocation].position;
            }
            context.agent.SetDestination(walkingPosition);
            return State.Running;
        }        
        return State.Running;
    }
}
