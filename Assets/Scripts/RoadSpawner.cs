using AmazingAssets.CurvedWorld.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public enum AXIS { XPositive, XNegative, ZPositive, ZNegative }

    public GameObject[] roads;
    public int initialSpawnCount = 5;
    public float destoryZone = 300;

    [Space(10)]
    public AXIS axis;

    [HideInInspector]
    public Vector3 moveDirection = new Vector3(-1, 0, 0);


    public float roadSize = 60;
    GameObject lastChunk;
    GameMode gameMode;


    void Start()
    {
        gameMode = GameObject.Find("GameMode").GetComponent<GameMode>();
        initialSpawnCount = initialSpawnCount > roads.Length ? initialSpawnCount : roads.Length;

        int chunkIndex = 0;
        for (int i = 0; i < initialSpawnCount; i++)
        {
            GameObject chunk = (GameObject)Instantiate(roads[chunkIndex]);
            chunk.SetActive(true);

            chunk.GetComponent<MovingRoadController>().spawner = this;
            chunk.GetComponent<MovingRoadController>().movingSpeed = gameMode.player.moveSpeed;

            switch (axis)
            {
                case AXIS.XPositive:
                    chunk.transform.localPosition = new Vector3(-i * roadSize, 0, transform.position.z);
                    moveDirection = new Vector3(1, 0, 0);
                    break;

                case AXIS.XNegative:
                    chunk.transform.localPosition = new Vector3(i * roadSize, 0, transform.position.z);
                    moveDirection = new Vector3(-1, 0, 0);
                    break;

                case AXIS.ZPositive:
                    chunk.transform.localPosition = new Vector3(i * roadSize, 0, transform.position.z);
                    break;

                case AXIS.ZNegative:
                    chunk.transform.localPosition = new Vector3(i * roadSize, 0, transform.position.z);
                    break;
            }


            lastChunk = chunk;

            if (++chunkIndex >= roads.Length)
                chunkIndex = 0;
        }
    }

    public void DestroyRoad(MovingRoadController road)
    {
        Vector3 newPos = lastChunk.transform.position;
        switch (axis)
        {
            case AXIS.XPositive:
                newPos.x -= roadSize;
                break;

            case AXIS.XNegative:
                newPos.x += roadSize;
                break;

            case AXIS.ZPositive:
                break;

            case AXIS.ZNegative:
                break;
        }



        lastChunk = road.gameObject;
        lastChunk.transform.position = newPos;
    }
}
