using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameMode gameMode;
    Vector3 initialPos;
    Vector3 targetPos;
    Skybox skyBox;
    float skyboxRotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameMode = GameObject.Find("GameMode").GetComponent<GameMode>();
        initialPos = transform.position;
        targetPos = transform.position;
        skyBox = GetComponent<Skybox>();
    }

    // Update is called once per frame
    void Update()
    {
        skyboxRotation += Time.deltaTime;
        if (skyboxRotation > 360.0)
        {
            skyboxRotation -= 360f;
        }
        skyBox.material.SetFloat("_Rotation", skyboxRotation);
        if (!gameMode.isGameRunning)
        {
            return;
        }
        float crateNums = Mathf.Max(gameMode.player.allCarriedCrates.Count, gameMode.player2.allCarriedCrates.Count) - gameMode.player.initialCrateNum;
        targetPos = initialPos - transform.forward * crateNums * 0.75f;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
        
    }
}
