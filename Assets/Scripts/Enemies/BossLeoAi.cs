using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossLeoAi : Enemy
{
    [SerializeField] int fullArmor;
    [SerializeField] ParticleSystem brokeFx;
    [SerializeField] GameObject projectile1;
    [SerializeField] GameObject bossUi;
    [SerializeField] Slider lifeSlider;
    [SerializeField] Slider armorSlider;
    

    void Update()
    {
        if(bossFight == true)
        {
            bossUi.SetActive(true);
        }
        else
        {
            bossUi.SetActive(false);
        }
    }
    public override void TakeDamage(int damage, bool melee)
    {
        if(broke)
        {
            if(melee)
            {
                life -= damage;
                armor = fullArmor;
                broke = false;
                brokeFx.Stop();
            }
        }
        if(canTake == true)
        {
            armor -= damage;
        }
        if(armor <= 0)
        {
            broke = true;
            brokeFx.Play();
        }
    }
    public void RegenerateArmor()
    {
        armor = fullArmor;
        brokeFx.Stop();
    }

    void Start()
    {
        canTake = true;
        lifeSlider.maxValue = life;
        lifeSlider.value = life;
        armorSlider.maxValue = fullArmor;
        armorSlider.value = armor;
        
    }

    
    
}
