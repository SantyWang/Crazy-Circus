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

    private GameObject playerStart1;
    private GameObject playerStart2;

    BarrierSpawner barrierSpawner;
    private UIController uiController;

    // Start is called before the first frame update
    void Awake()
    {
        uiController = GameObject.Find("UI").GetComponent<UIController>();
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
        isGameRunning = true;

        if (player)
        {
            player.gameObject.SetActive(false);
            GameObject.Destroy(player.gameObject);
        }
        if (player2)
        {
            player2.gameObject.SetActive(false);
            GameObject.Destroy(player2.gameObject);
        }

        // 生成人物
        playerStart1 = GameObject.Find("PlayerStart1");
        if (playerStart1 != null )
        {
            player = Instantiate(playerPrefab).GetComponent<PlayerController>();
            player.transform.localPosition = playerStart1.transform.localPosition;
            player.transform.rotation = playerStart1.transform.rotation;
            player.moveSpeed = playerMovingSpeed;
        }
        playerStart2 = GameObject.Find("PlayerStart2");
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

        var BarrierSpawner = GameObject.Find("Barrier Spawner");
        if (BarrierSpawner != null)
        {
            barrierSpawner = BarrierSpawner.GetComponent<BarrierSpawner>();
            barrierSpawner.StartGame();
        }

        var GrabSpawner = GameObject.Find("Grab Spawner");
        if (GrabSpawner != null)
        {
            var grabSpawner = GrabSpawner.GetComponent<GrabSpawner>();
            grabSpawner.StartGame();
        }

        // 控制 UI Panel
        uiController.EnterGamePanel();
        uiController.UpdateGrabPanel(player);
        uiController.UpdateGrabPanel(player2);
    }

    public void StopGame()
    {
        if (!isGameRunning)
        {
            return;
        }
        isGameRunning = false;


        if (crateSpawner != null)
        {
            crateSpawner.StopGame();
        }

        if (barrierSpawner != null)
        { 
            barrierSpawner.StopGame();
        }

        Debug.Log("EndGame");

        // 控制 UI Panel
        uiController.EnterEndPanel();
        player.disableInput();
        player2.disableInput();
    }

    public void EndGame()
    {
        // 控制 UI Panel
        uiController.EnterEndPanel();
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
