using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMaxLifeSpell : MonoBehaviour, IInteractable
{
    [SerializeField] int maxLifeBonus;

    public void Interact(GameObject player)
    {
        player.GetComponent<CombatManager>().IncreaseMaxLife(maxLifeBonus);
        player.GetComponent<Interact>().RemoveFromInteract();
        Destroy(gameObject);
    }
}
