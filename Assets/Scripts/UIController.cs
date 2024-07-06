using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{

    GameMode gameMode;

    GameObject startPanel;
    GameObject gamePanel;
    GameObject endPanel;
    GameObject playerAGarbsPanel;
    GameObject playerBGarbsPanel;

    // Start is called before the first frame update
    void Start()
    {
        gameMode = GameObject.Find("GameMode").GetComponent<GameMode>();

        startPanel = GameObject.Find("StartPanel");
        gamePanel = GameObject.Find("GamePanel");
        endPanel = GameObject.Find("EndPanel");

        playerAGarbsPanel = GameObject.Find("playerAGrabs");
        playerBGarbsPanel = GameObject.Find("playerBGrabs");

        this.EnterStartPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameStarted()
    {
        gameMode.StartGame();
    }

    public void OnGamePaused ()
    {
        gameMode.PauseGame();
    }

    public void OnGameResume()
    {
        gameMode.ResumeGame();
    }

    public void OnGameStoped ()
    {
        gameMode.StopGame();
    }

    public void EnterStartPanel()
    {
        startPanel.SetActive(true);
        gamePanel.SetActive(false);
        endPanel.SetActive(false);
    }

    public void EnterEndPanel()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(false);
        endPanel.SetActive(true);
    }

    public void EnterGamePanel()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        endPanel.SetActive(false);
    }

    public void UpdateGrabPanel(PlayerController player)
    {
        var playerGarbsPanel = player.name.Contains("Player1") ? playerAGarbsPanel : playerBGarbsPanel;
        

        // 没有道具时
        if (player.grablist.Count == 0)
        {
            // 隐藏grab1
            playerGarbsPanel.gameObject.SetActive(false);
            return;
        }
        //在该panel下查找Grab1节点
        
        playerGarbsPanel.gameObject.SetActive(true);

        var banana = playerGarbsPanel.transform.Find("banana");
        var box = playerGarbsPanel.transform.Find("box");
        var collision = playerGarbsPanel.transform.Find("collision");
        var boom = playerGarbsPanel.transform.Find("boom");

        banana.gameObject.SetActive(false);
        box.gameObject.SetActive(false);
        collision.gameObject.SetActive(false);
        boom.gameObject.SetActive(false);

        var grab = player.grablist.ElementAt(0);
        grab = grab.Contains("(Clone)") ? grab.Replace("(Clone)", "") : grab;

        switch (grab)
        {
            case "banana1":
            case "banana2":
                banana.gameObject.SetActive(true);
                break;
            case "box1":
            case "box2":
            case "box3":
                box.gameObject.SetActive(true);
                break;
            case "collision":
            case "collision2":
                collision.gameObject.SetActive(true);
                break;
            case "boom":
                boom.gameObject.SetActive(true);
                break;
        }

    }
}
