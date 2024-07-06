using AmazingAssets.CurvedWorld.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSpawner : MonoBehaviour
{
    public GameObject[] crates;
    public float spawnRate = 1;

    [Range(0f, 1f)]
    public float spawnRandomizer = 0.5f;

    [Space(10)]
    public Vector3 positionRandomizer = new Vector3(0, 0, 0);
    public Vector3 rotation = new Vector3(0, 90, 0);


    [Space(10)]
    public Vector3 moveDirection = new Vector3(1, 0, 0);

    private GameMode gameMode;


    float deltaTime;

    public void StartGame()
    {
        deltaTime = 0;
        gameMode = GameObject.Find("GameMode").GetComponent<GameMode>();
    }

    public void StopGame()
    {
        deltaTime = 0;
        gameMode = null;
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += Time.deltaTime;

        if (gameMode && deltaTime > spawnRate)
        {
            deltaTime = 0;

            if (Random.value > spawnRandomizer)
            {
                int index = Random.Range(0, crates.Length);

                GameObject carObject = Instantiate(crates[index]);
                carObject.SetActive(true);

                carObject.transform.position = transform.position + Vector3.Scale(Random.insideUnitSphere, positionRandomizer);
                carObject.transform.rotation = Quaternion.Euler(rotation);

                MovingCrateController movingCrateController = carObject.GetComponent<MovingCrateController>();
                movingCrateController.moveDirection = moveDirection;
                movingCrateController.movingSpeed = gameMode.player.moveSpeed;
            }
        }
    }

}
