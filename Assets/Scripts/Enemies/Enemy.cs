using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]public float life;
    [SerializeField]protected float armor;
    [SerializeField]public Transform projectilePlace;
    [SerializeField]public Renderer enemyRenderer;
    public bool broke;
    public bool dead;
    public bool canTake;
    public bool bossFight;
    public abstract void TakeDamage(int damage, bool melee);
}
