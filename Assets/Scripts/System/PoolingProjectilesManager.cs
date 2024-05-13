using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PoolingProjectilesManager : Singleton<PoolingProjectilesManager>
{
    public Transform inactiveProjectilePlace;
    [Header("Player Projectiles")]
    [SerializeField]private List<GameObject> basicProjectiles;
    [SerializeField] int currentBasicProjectile;
    [SerializeField]private List<GameObject> LightBallProjectiles;
    [SerializeField] int currentLightBallProjectile;
    [Header("Enemies projectiles")]
    [SerializeField]private List<GameObject> enemySoundProjectiles;
    [SerializeField] int currentEnemyProjectile;

    [SerializeField] GameObject basicProjectile;
    [SerializeField] GameObject explosiveProjectile;
    [SerializeField] GameObject enemyProjectile;



    protected override void Awake()
    {
        //if I call the base.awake it will be not destroyed on load and will destroy other types of it
        //base.Awake
    }

    public void SetProjectiles()
    {
        basicProjectiles.Clear();
        LightBallProjectiles.Clear();
        enemySoundProjectiles.Clear();
        inactiveProjectilePlace = transform;
        for (int i = 0; i <3; i++)
        {
            enemySoundProjectiles.Add(Instantiate(enemyProjectile, inactiveProjectilePlace.position, quaternion.identity));
        }
        for (int i = 0; i < 3; i++)
        {
            basicProjectiles.Add(Instantiate(basicProjectile, inactiveProjectilePlace.position, quaternion.identity));
        }
        for (int i = 0; i < 3; i++)
        {
            LightBallProjectiles.Add(Instantiate(explosiveProjectile, inactiveProjectilePlace.position, quaternion.identity));
        }
    }

    public GameObject ThrowProjectileBasic()
    {
        if (currentBasicProjectile >= basicProjectiles.Count - 1)
        {
            currentBasicProjectile = 0;
            return basicProjectiles[currentBasicProjectile];
        }
        currentBasicProjectile++;
        return basicProjectiles[currentBasicProjectile];
    }

    public GameObject ThrowProjectileLightBall()
    {
        if (currentLightBallProjectile >= LightBallProjectiles.Count - 1)
        {
            currentLightBallProjectile = 0;
            return LightBallProjectiles[currentLightBallProjectile];
        }
        currentLightBallProjectile++;
        return LightBallProjectiles[currentLightBallProjectile];
    }
    public GameObject ThrowProjectileEnemySound()
    {
        if (currentEnemyProjectile >= enemySoundProjectiles.Count - 1)
        {
           currentEnemyProjectile = 0;
            return enemySoundProjectiles[currentEnemyProjectile];
        }
        currentEnemyProjectile++;
        return enemySoundProjectiles[currentEnemyProjectile];
    }

}
