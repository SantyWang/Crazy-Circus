using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject playerPrefab2;
    [HideInInspector]
    public PlayerController player;
    [HideInInspector]
    public PlayerController player2;
    public float playerMovingSpeed = 5;
    private CrateSpawner crateSpawner;
    [HideInInspector]
    public bool isGameRunning = false;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (isGameRunning)
        {
            return;
        }
        // 生成人物
        var playerStart1 = GameObject.Find("PlayerStart1");
        playerStart1.SetActive(true);
        if (playerStart1 != null )
        {
            player = Instantiate(playerPrefab).GetComponent<PlayerController>();
            player.transform.localPosition = playerStart1.transform.localPosition;
            player.transform.rotation = playerStart1.transform.rotation;
            player.moveSpeed = playerMovingSpeed;
        }
        var playerStart2 = GameObject.Find("PlayerStart2");
        playerStart2.SetActive(true);
        if (playerStart2 != null)
        {
            player2 = Instantiate(playerPrefab2).GetComponent<PlayerController>();
            player2.transform.localPosition = playerStart2.transform.localPosition;
            player2.transform.rotation = playerStart2.transform.rotation;
            player2.moveSpeed = playerMovingSpeed;
        }

        // 开始生成道具 / 开始生成道路
        var CrateSpawner = GameObject.Find("Crate Spawner");
        if (CrateSpawner != null) {
            crateSpawner = CrateSpawner.GetComponent<CrateSpawner>();
            crateSpawner.StartGame();
        }

        var GrabSpawner = GameObject.Find("Grab Spawner");
        if (GrabSpawner != null)
        {
            var grabSpawner = GrabSpawner.GetComponent<GrabSpawner>();
            grabSpawner.StartGame();
        }
        isGameRunning = true;
    }

    public void StopGame()
    {
        if (!isGameRunning)
        {
            return;
        }
        var playerStart1 = GameObject.Find("PlayerStart1");
        playerStart1.SetActive(false);
        var playerStart2 = GameObject.Find("PlayerStart2");
        playerStart2.SetActive(false);
        if (crateSpawner != null)
        {
            crateSpawner.StopGame();
        }
        isGameRunning = false;
    }

    public void PauseGame()
    {
        // if (crateSpawner !== null)
        // {
        //     crateSpawner.PauseGame();
        // }
    }
    public void ResumeGame()
    {
        // if (crateSpawner !== null)
        // {
        //     crateSpawner.ResumeGame();
        // }
    }
}
