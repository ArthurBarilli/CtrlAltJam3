using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]protected float life;
    [SerializeField]protected float armor;
    [SerializeField]public Transform projectilePlace;
    public bool broke;
    public bool dead;
    public abstract void TakeDamage(int damage, bool melee);
}
