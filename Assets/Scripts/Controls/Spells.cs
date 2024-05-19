using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spells : MonoBehaviour, IInteractable
{
   public abstract void Attack(Vector3 direction, Vector3 origin);
   public Sprite spellSprite;
   [SerializeField]public float manaCost;
   public int bonusDamage;
   public float fireRate;
   [SerializeField] Animator anim;
    public bool interactable { get; set; }

    void Update()
    {
        if(interactable)
        {
            anim.SetBool("Interact", true);
        }
        else
        {
            anim.SetBool("Interact", false);
        }
    }

    public void Interact(GameObject player)
    {
        player.GetComponent<CombatManager>().AddSpell(gameObject);
    }
}
