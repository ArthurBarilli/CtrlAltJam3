using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMaxManaSpell : MonoBehaviour, IInteractable
{
    [SerializeField] float maxManaBonus;
    public void Interact(GameObject player)
    {
        player.GetComponent<CombatManager>().IncreaseMaxMana(maxManaBonus);
        player.GetComponent<Interact>().RemoveFromInteract();
        Destroy(gameObject);
    }
}
