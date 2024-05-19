using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateSpell : MonoBehaviour, IInteractable
{
    [SerializeField] float fireRateIncrease;//decimal that represents percentage
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
        player.GetComponent<CombatManager>().IncreaseFireRate(fireRateIncrease);
        player.GetComponent<Interact>().RemoveFromInteract();
        Destroy(gameObject);
    }
}
