using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CanvaRotation : MonoBehaviour
{
    [SerializeField] GameObject parentObject;
    Quaternion desiredRotation;
    Quaternion currentRotation;
    void Start()
    {
        desiredRotation = Quaternion.LookRotation(Camera.main.transform.position - parentObject.transform.position, Camera.main.transform.up);
    }
void LateUpdate()
   {
        currentRotation = transform.rotation;
        if (currentRotation != desiredRotation)
        {
            transform.rotation = desiredRotation;
            transform.RotateAround(parentObject.transform.position, parentObject.transform.rotation * Vector3.up, 0);
        }
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position, Camera.main.transform.up);
    }

}
