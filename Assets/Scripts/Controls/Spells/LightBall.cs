using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LightBall : Spells
{
    [SerializeField] GameObject lightBallProjectile;
    [SerializeField] float projectileSpeed;
    

    public override void Attack(Vector3 direction, Vector3 attackOrigin)
    {
        GameObject projectile = PoolingProjectilesManager.Instance.ThrowProjectileLightBall();
        projectile.GetComponent<LightBallProjectile>().damage += bonusDamage;
        projectile.transform.position = attackOrigin;
        projectile.GetComponent<LightBallProjectile>().ActivateProjectile();
        projectile.GetComponent<Rigidbody>().velocity = direction.normalized * projectileSpeed;
    }
}
