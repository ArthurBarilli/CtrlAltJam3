using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;



public class GameManager : Singleton<GameManager>
{
    
    // Start is called before the first frame update
    [SerializeField] GameObject playerPrefab;
    [SerializeField] CinemachineVirtualCamera vCamera;
    [SerializeField] List<Transform> playerSpawns;
    [SerializeField] List<Transform> itemSpawns;
    [SerializeField] List<Collider> enemiesSpawns;
    [SerializeField] List<GameObject> items;
    [SerializeField] GameObject GameOverBg;
    [SerializeField] GameObject currentBoss;
    [SerializeField] GameObject currentPlayer;
    [SerializeField] GameObject bossPrefab;
    [SerializeField] List<GameObject> enemies;
    [SerializeField] Transform bossSpawn;
    [SerializeField] List<Transform> objectives;

    [Header("Objectives")]
    [SerializeField] float closestDistance = Mathf.Infinity;
    [SerializeField] Transform closestObjective = null;
    [SerializeField] NavMeshAgent directionAgent;
    [SerializeField] GameObject directionObject;


    protected override void Awake()
    {
        //if I call the base.awake it will be not destroyed on load and will destroy other types of it
        base.Awake();
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        SceneManager.sceneLoaded += OnSceneLoaded;
        StartRun();
        StartCoroutine(ClosestObjective());
    }

    public void PlayerDie()
    {
        GameOverBg.SetActive(true);
    }
    public void StartRun()
    {
        //reloads the scene
        SceneManager.LoadScene(Scenes.GameplayScene.ToString());
        GameOverBg.SetActive(false);
    }

    IEnumerator SpawnsRoutine()
    {
        yield return  new WaitForFixedUpdate();
        //selects a random spawn
        int randomSpawn = Random.Range(0, playerSpawns.Count);
        //instantiates the player
        currentPlayer = Instantiate(playerPrefab, playerSpawns[randomSpawn].position, Quaternion.identity);
        currentPlayer.GetComponent<CombatManager>().mainCamera = Camera.main;
        vCamera.LookAt = currentPlayer.transform;
        vCamera.Follow = currentPlayer.transform;
        //sets the projectiles
        GetComponent<PoolingProjectilesManager>().SetProjectiles();
        //sets the items
        foreach (Transform itemSpawn in itemSpawns)
        {
            int randomItem = Random.Range(0, items.Count);
            objectives.Add(Instantiate(items[randomItem], itemSpawn.position, Quaternion.identity).transform);
        }
        //sets the enemies
        foreach (Collider spawns in enemiesSpawns)
        {
            int randomNumber = Random.Range(3,6);
            SpawnEnemies(spawns, randomNumber);
        }
        //sets the boss
        currentBoss = Instantiate(bossPrefab, bossSpawn.position, Quaternion.identity);
        objectives.Add(currentBoss.transform);
        //sets the objective Object

        //sets the position of the boss and the boss arena

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == Scenes.GameplayScene.ToString())
        {
            StartCoroutine(SpawnsRoutine());
        }
    }

    public void StartBossFight()
    {
        currentBoss.GetComponent<BossLeoAi>().bossFight = true;
    }
    IEnumerator ClosestObjective()
    {
        yield return new WaitForSeconds(10f);
        //finds the closest objective

        foreach(Transform objective in objectives)
        {
            float distance = Vector3.Distance(currentPlayer.transform.position, objective.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObjective = objective;
            }
        }
        CheckClosestAgain();
    }
    void CheckClosestAgain()
    {
        StartCoroutine(ClosestObjective());
    }

    public void DirectPlayer()
    {
        StopCoroutine(DirectionTime());
        directionObject.transform.position = currentPlayer.transform.position;
        directionObject.SetActive(true);
        directionAgent.SetDestination(closestObjective.position);
        StartCoroutine(DirectionTime());
    }

    IEnumerator DirectionTime()
    {
        yield return new WaitForSeconds(3f);
        directionAgent.ResetPath();
        directionObject.SetActive(false);   
    }

    private void SpawnEnemies(Collider spawnSpace, int numberOfCharacters)
    {
        Vector3 size = spawnSpace.bounds.size;
        float area = size.x * size.z;
        for (int i = 0; i < numberOfCharacters; i++)
        {
            int randomEnemy = Random.Range(0, enemies.Count);
            Vector3 spawnPosition = spawnSpace.bounds.center + new Vector3(Random.Range(-size.x/2, size.x/2), 0, Random.Range(-size.z/2, size.z/2));
            Instantiate(enemies[randomEnemy], spawnPosition, Quaternion.identity);
        }
    }


}
