using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicSpellProjectile : MonoBehaviour
{
    public int damage;
    public bool active = false;
    [SerializeField] float lifeTime;
    float counter;

    void Update()
    {
        if (active)
        {
            if(counter <= lifeTime)
            {
                counter += Time.deltaTime;
            }
            else
            {
                DeactivateProjectile();
                active = false;
                counter = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(active)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(damage, false);
                DeactivateProjectile();
            }
        }

    }

    public void ActivateProjectile()
    {
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


}
