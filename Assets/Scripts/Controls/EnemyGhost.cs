using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGhost : Enemy
{
    [SerializeField] GameObject player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject eyes;
    [SerializeField] GameObject brokeFx;
    [SerializeField] bool waiting;
    [SerializeField] float damageCd;
    [SerializeField] BoxCollider dmgCollider;


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
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
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

        


        if(!waiting)
        {
            if(!broke)
            {
                //goes to player direction
                agent.SetDestination(player.transform.position);
                //points the eyes to the camera
                eyes.transform.LookAt(Camera.main.transform);
            }
            else
            {
                agent.ResetPath();
            }
        }
        else
        {
            agent.ResetPath();
        }

        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            waiting = true;
            other.GetComponent<CombatManager>().TakeDamage(1);
            StartCoroutine(dmgCD());
            dmgCollider.enabled = false;
        }
    }

    IEnumerator dmgCD()
    {
        yield return new WaitForSeconds(damageCd);
        waiting = false;
        dmgCollider.enabled = true;
    }
}
