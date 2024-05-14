using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLeoAi : Enemy
{
    public override void TakeDamage(int damage, bool melee)
    {
        if(broke)
        {
            if(melee)
            {
                life -= damage;
            }
        }
        if(canTake == true)
        {
            armor -= damage;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
