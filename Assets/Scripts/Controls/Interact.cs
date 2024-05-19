using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] KeyCode interactInput;
    [SerializeField] IInteractable InteractableObject;


    void Update()
    {
        //Debug.Log(InteractableObject);
        if (Input.GetKeyDown(interactInput)  && InteractableObject != null)
        {
            InteractableObject.Interact(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Interactable"))
        {
            InteractableObject = other.GetComponent<IInteractable>();
            InteractableObject.interactable = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Interactable"))
        {
            InteractableObject.interactable = false;
            InteractableObject = null;
            
        }
    }

    public void RemoveFromInteract()
    {
        InteractableObject = null;
    }
}
