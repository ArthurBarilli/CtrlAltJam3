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
    [SerializeField] bool started;
    [SerializeField] float damageCd;
    [SerializeField] BoxCollider dmgCollider;
    [SerializeField] Slider lifeSlider;
    [SerializeField] Slider armorSlider;
    [SerializeField] ParticleSystem hitFx;
    [SerializeField] GameObject particles;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
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
            hitFx.Play();
            armor -= damage;
        }
    }
    void Update()
    {
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
            particles.SetActive(false);
            eyes.SetActive(false);
            brokeFx.SetActive(true);
        }

        if(Vector3.Distance(player.transform.position, transform.position) > 40)
        {
            started = false;
            enemyRenderer.gameObject.SetActive(false);
        }
        else if(Vector3.Distance(player.transform.position, transform.position) < 40 && Vector3.Distance(player.transform.position, transform.position) > 20)
        {
            started = false;
            enemyRenderer.gameObject.SetActive(true);
        }
        else if(Vector3.Distance(player.transform.position, transform.position) < 20)
        {
            started = true;
        }

        if(!waiting && started)
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
        if(other.CompareTag("Player") && !broke)
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
