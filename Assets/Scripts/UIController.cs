using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    GameMode gameMode;

    GameObject startPanel;
    GameObject gamePanel;
    GameObject endPanel;

    // Start is called before the first frame update
    void Start()
    {
        gameMode = GameObject.Find("GameMode").GetComponent<GameMode>();

        startPanel = GameObject.Find("StartPanel");
        gamePanel = GameObject.Find("GamePanel");
        endPanel = GameObject.Find("EndPanel");

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
}
