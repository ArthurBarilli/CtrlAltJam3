using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spells : MonoBehaviour, IInteractable
{
   public abstract void Attack(Vector3 direction, Vector3 origin);
   [SerializeField]public float manaCost;
   public float fireRate;

    public void Interact(GameObject player)
    {
        player.GetComponent<CombatManager>().AddSpell(gameObject);
    }
}
