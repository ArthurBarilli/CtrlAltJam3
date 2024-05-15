using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightSetup : MonoBehaviour
{
    public List<Transform> enemiesSpawn;
    public List<GameObject> enemies;
    public Transform fightCenter;
    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemies()
    {
        foreach (Transform place in enemiesSpawn)
        {
            int randomEnemy = Random.Range(0,2);
            Instantiate(enemies[randomEnemy], place.position, Quaternion.identity);
        }
    }
}
