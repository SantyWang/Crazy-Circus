using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    GameMode gameMode;
    // Start is called before the first frame update
    void Start()
    {
        gameMode = GameObject.Find("GameMode").GetComponent<GameMode>();
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
}
