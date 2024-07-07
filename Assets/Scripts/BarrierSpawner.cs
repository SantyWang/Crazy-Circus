using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierSpawner : MonoBehaviour
{
    public GameObject[] barriers;
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
                int index = Random.Range(0, barriers.Length);

                GameObject barrier = Instantiate(barriers[index]);
                barrier.SetActive(true);

                barrier.transform.position = transform.position + Vector3.Scale(Random.insideUnitSphere, positionRandomizer);
                barrier.transform.rotation = Quaternion.Euler(rotation);

                BarrierController barrierController = barrier.GetComponent<BarrierController>();
                barrierController.moveDirection = moveDirection;
                barrierController.movingSpeed = gameMode.playerMovingSpeed;
            }
        }
    }
}
