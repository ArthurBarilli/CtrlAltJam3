using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : Singleton<GameManager>
{
    
    // Start is called before the first frame update
    [SerializeField] GameObject playerPrefab;
    [SerializeField] CinemachineVirtualCamera vCamera;
    [SerializeField] List<Transform> spawns;
    [SerializeField] GameObject GameOverBg;

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
        int randomSpawn = Random.Range(0, spawns.Count);
        //instantiates the player
        GameObject currentPlayer = Instantiate(playerPrefab, spawns[randomSpawn].position, Quaternion.identity);
        currentPlayer.GetComponent<CombatManager>().mainCamera = Camera.main;
        vCamera.LookAt = currentPlayer.transform;
        vCamera.Follow = currentPlayer.transform;
        //sets the projectiles
        GetComponent<PoolingProjectilesManager>().SetProjectiles();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == Scenes.GameplayScene.ToString())
        {
            StartCoroutine(SpawnsRoutine());
        }
    }
}
