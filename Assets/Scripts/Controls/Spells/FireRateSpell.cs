using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateSpell : MonoBehaviour, IInteractable
{
    [SerializeField] float fireRateIncrease;//decimal that represents percentage
    [SerializeField] Animator anim;
    public bool interactable { get; set; }
    [SerializeField] GameObject canInteractFx;
    [SerializeField] GameObject cantInteractFx;

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
        player.GetComponent<CombatManager>().IncreaseFireRate(fireRateIncrease);
        player.GetComponent<Interact>().RemoveFromInteract();
        Destroy(gameObject);
    }
}
