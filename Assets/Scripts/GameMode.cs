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

    private CrateSpawner crateSpawner;

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
        // 生成人物
        var playerStart1 = GameObject.Find("PlayerStart1");
        playerStart1.SetActive(true);
        if (playerStart1 != null )
        {
            player = Instantiate(playerPrefab).GetComponent<PlayerController>();
            player.transform.localPosition = playerStart1.transform.localPosition;
            player.transform.rotation = playerStart1.transform.rotation;
        }
        var playerStart2 = GameObject.Find("PlayerStart2");
        playerStart2.SetActive(true);
        if (playerStart2 != null)
        {
            player2 = Instantiate(playerPrefab2).GetComponent<PlayerController>();
            player2.transform.localPosition = playerStart2.transform.localPosition;
            player2.transform.rotation = playerStart2.transform.rotation;
        }

        // 开始生成道具 / 开始生成道路
        var CrateSpawner = GameObject.Find("Crate Spawner");
        if (CrateSpawner != null) {
            crateSpawner = CrateSpawner.GetComponent<CrateSpawner>();
            crateSpawner.StartGame();
        }
    }

    public void StopGame()
    {
        var playerStart1 = GameObject.Find("PlayerStart1");
        playerStart1.SetActive(false);
        var playerStart2 = GameObject.Find("PlayerStart2");
        playerStart2.SetActive(false);
        if (crateSpawner != null)
        {
            crateSpawner.StopGame();
        }
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
