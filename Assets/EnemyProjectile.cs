using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    [SerializeField] int damage;
    public bool active = false;
    [SerializeField] float lifeTime;
    [SerializeField] GameObject soundWaveVfx;
    float counter;


    // Update is called once per frame
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
        if (other.CompareTag("Player") && active == true)
        {
            other.GetComponent<CombatManager>().TakeDamage(damage);
            DeactivateProjectile();
        }
    }

    public void ActivateProjectile()
    {
        counter = 0;
        active = true;
        soundWaveVfx.SetActive(true);
        //activate further options
    }

    public void DeactivateProjectile()
    {
        active = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = PoolingProjectilesManager.Instance.inactiveProjectilePlace.position;
        //deactivate further options
        soundWaveVfx.SetActive(false);
    }
}
