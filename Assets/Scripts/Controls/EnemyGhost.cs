using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyGhost : Enemy
{
    [SerializeField] GameObject player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject eyes;
    [SerializeField] GameObject brokeFx;
    [SerializeField] bool waiting;
    [SerializeField] float damageCd;
    [SerializeField] BoxCollider dmgCollider;
    [SerializeField] Canvas canva;
    [SerializeField] Transform canvaObject;
    [SerializeField] Slider lifeSlider;
    [SerializeField] Slider armorSlider;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        canva.worldCamera = Camera.main;
        lifeSlider.maxValue = life;
        armorSlider.maxValue = armor;
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
        canvaObject.LookAt(Camera.main.transform.position);
        lifeSlider.value = life;
        armorSlider.value = armor;
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
