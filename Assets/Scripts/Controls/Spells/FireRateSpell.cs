using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateSpell : MonoBehaviour, IInteractable
{
    [SerializeField] float fireRateIncrease;//decimal that represents percentage
    public void Interact(GameObject player)
    {
        player.GetComponent<CombatManager>().IncreaseFireRate(fireRateIncrease);
        player.GetComponent<Interact>().RemoveFromInteract();
        Destroy(gameObject);
    }
}
