using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyGhostRound : Enemy
{
    [SerializeField] GameObject player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject eyes;
    [SerializeField] GameObject brokeFx;
    [SerializeField] Slider lifeSlider;
    [SerializeField] Slider armorSlider;

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
            lifeSlider.value = life;
        }
        else
        {
            armor -= damage;
            armorSlider.value = armor;
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
