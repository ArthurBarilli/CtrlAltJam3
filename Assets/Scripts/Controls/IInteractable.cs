using UnityEngine;

public interface IInteractable
{
    bool interactable{ get; set; }
    void Interact(GameObject player);
}
