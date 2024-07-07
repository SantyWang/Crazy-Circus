using AmazingAssets.CurvedWorld.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRoadController : MonoBehaviour
{
    [HideInInspector]
    public RoadSpawner spawner;
    [HideInInspector]
    public float movingSpeed;

    [HideInInspector]
    public Vector3 offest = new Vector3(0,0,0);

    private void Start()
    {
        offest = spawner.moveDirection * movingSpeed;
        transform.Translate(offest * 20);
    }
    void Update()
    {
        transform.Translate(spawner.moveDirection * movingSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        switch (spawner.axis)
        {
            case RoadSpawner.AXIS.XPositive:
                if (transform.position.x > spawner.destoryZone)
                    spawner.DestroyRoad(this);
                break;

            case RoadSpawner.AXIS.XNegative:
                if (transform.position.x < -spawner.destoryZone)
                    spawner.DestroyRoad(this);
                break;

            case RoadSpawner.AXIS.ZPositive:
                if (transform.position.z > spawner.destoryZone)
                    spawner.DestroyRoad(this);
                break;

            case RoadSpawner.AXIS.ZNegative:
                if (transform.position.z < -spawner.destoryZone)
                    spawner.DestroyRoad(this);
                break;
        }

    }
}
