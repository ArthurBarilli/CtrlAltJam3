using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpell : Spells
{
    [SerializeField] GameObject basicSpellProjectile;
    [SerializeField] float projectileSpeed;
    public override void Attack(Vector3 direction, Vector3 attackOrigin)
    {
        GameObject projectile = PoolingProjectilesManager.Instance.ThrowProjectileBasic();
        projectile.GetComponent<BasicSpellProjectile>().damage += bonusDamage;
        projectile.transform.position = attackOrigin;
        projectile.GetComponent<BasicSpellProjectile>().ActivateProjectile();
        projectile.GetComponent<Rigidbody>().velocity = direction.normalized * projectileSpeed;
    }
}
