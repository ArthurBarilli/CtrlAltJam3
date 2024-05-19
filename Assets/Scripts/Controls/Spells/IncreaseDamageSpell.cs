using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamageSpell : MonoBehaviour, IInteractable
{
    [SerializeField] int damageIncrease;
    [SerializeField] Animator anim;
    [SerializeField] GameObject canInteractFx;
    [SerializeField] GameObject cantInteractFx;


    public bool interactable { get; set; }

    void Update()
    {
        if(interactable)
        {
            anim.SetBool("Interact", true);
            canInteractFx.SetActive(true);
            cantInteractFx.SetActive(false);
        }
        else
        {
            anim.SetBool("Interact", false);
            canInteractFx.SetActive(false);
            cantInteractFx.SetActive(true);
        }
    }

    public void Interact(GameObject player)
    {
        player.GetComponent<CombatManager>().IncreaseDamage(damageIncrease);
        player.GetComponent<Interact>().RemoveFromInteract();
        Destroy(gameObject);
    }
}
