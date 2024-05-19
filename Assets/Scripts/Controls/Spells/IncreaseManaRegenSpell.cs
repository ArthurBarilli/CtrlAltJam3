using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseManaRegenSpell : MonoBehaviour, IInteractable
{
    [SerializeField] float bonusValue1; //increase in the mana per second regen
    [SerializeField] float bonusValue2; //decrease in the time without casting for the mana to start regenerating
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
        player.GetComponent<CombatManager>().IncreaseManaRegen(bonusValue1,bonusValue2);
        player.GetComponent<Interact>().RemoveFromInteract();
        Destroy(gameObject);
    }
}
