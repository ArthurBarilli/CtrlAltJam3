using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMaxLifeSpell : MonoBehaviour, IInteractable
{
    [SerializeField] int maxLifeBonus;
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
        player.GetComponent<CombatManager>().IncreaseMaxLife(maxLifeBonus);
        player.GetComponent<Interact>().RemoveFromInteract();
        Destroy(gameObject);
    }
}
