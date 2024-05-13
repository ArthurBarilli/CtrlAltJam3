using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGhostRound : Enemy
{
    [SerializeField] GameObject player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject eyes;
    [SerializeField] GameObject brokeFx;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        
        
    }
    public override void TakeDamage(int damage, bool melee)
    {
        if(broke && melee)
        {
            life -= damage;
        }
        else
        {
            armor -= damage;
        }
    }
    void Update()
    {

        if (life <= 0)
        {
            dead = true;
            Destroy(this.gameObject);
            Debug.Log("Dead");
        }
        if(armor <= 0)
        {
            broke = true;
            eyes.SetActive(false);
            brokeFx.SetActive(true);
        }


        
        //points the eyes to the camera
        eyes.transform.LookAt(Camera.main.transform);
    }
}
