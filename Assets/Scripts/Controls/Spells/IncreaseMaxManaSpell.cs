using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMaxManaSpell : MonoBehaviour, IInteractable
{
    [SerializeField] float maxManaBonus;
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
        player.GetComponent<CombatManager>().IncreaseMaxMana(maxManaBonus);
        player.GetComponent<Interact>().RemoveFromInteract();
        Destroy(gameObject);
    }
}
