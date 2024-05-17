using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamageSpell : MonoBehaviour, IInteractable
{
    [SerializeField] int damageIncrease;

    public void Interact(GameObject player)
    {
        player.GetComponent<CombatManager>().IncreaseDamage(damageIncrease);
        player.GetComponent<Interact>().RemoveFromInteract();
        Destroy(gameObject);
    }
}
