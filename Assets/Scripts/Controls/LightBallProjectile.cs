using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightBallProjectile : MonoBehaviour
{
    [SerializeField] float lifeTime;
    [SerializeField] float explodeTime;
    [SerializeField] float counter;
    public int damage;
    public bool active;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (active)
        {
            if(counter <= lifeTime)
            {
                counter += Time.deltaTime;
                if(counter >= explodeTime)
                {
                    Explode();
                }
            }
            else
            {
                DeactivateProjectile();
                active = false;
                counter = 0;
            }
        }
    }

    void Explode()
    {
        rb.velocity = Vector3.zero;
        GetComponent<Light>().range = 30;
        GetComponent<Light>().intensity = 10;
        GetComponent<MeshRenderer>().enabled = false;
    }

    public void ActivateProjectile()
    {
        GetComponent<Light>().range = 1;
        GetComponent<Light>().intensity = 1;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Light>().enabled = true;
        counter = 0;
        active = true;
        //activate further options
    }
    public void DeactivateProjectile()
    {
        GetComponent<Light>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = PoolingProjectilesManager.Instance.inactiveProjectilePlace.position;
        //deactivate further options
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Explode();
            other.GetComponent<Enemy>().TakeDamage(damage, false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            //dealDamage
        }
    }
}
