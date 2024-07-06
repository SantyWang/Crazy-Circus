using AmazingAssets.CurvedWorld.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrabSpawner : MonoBehaviour
{
    public GameObject[] crates;
    public float spawnRate = 1;

    private GameMode gameMode;

    [Range(0f, 1f)]
    public float spawnRandomizer = 0.5f;

    [Space(10)]
    public Vector3 positionRandomizer = new Vector3(0, 0, 0);
    public Vector3 rotation = new Vector3(0, 0, 0);

    [Space(10)]
    public Vector3 moveDirection = new Vector3(1, 0, 0);


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
                carObject.transform.rotation = Quaternion.Euler(rotation) * carObject.transform.rotation;

                MovingCrateController movingCrateController = carObject.GetComponent<MovingCrateController>();
                movingCrateController.moveDirection = moveDirection;
                movingCrateController.movingSpeed = gameMode.playerMovingSpeed;
            }
        }
    }

    public void useGrab(PlayerController from, PlayerController to, string grab)
    {
        if(from == null || to ==null || grab == null)
        {
            return;
        }
        string name = grab;
        // 如果name带了(cloned) 要去掉
        name = name.Replace("(Clone)", "");

        switch (name)
        {
            // 香蕉的效果是给对方朝自己的方向加速
            case "banana1":
            case "banana2":
                attract(from, to);
                break;
            // 惊吓魔盒的效果是让对方摇晃幅度增加
            case "box1":
                {
                    // 没想好
                    hurt(from, to);
                }
                break;
            // 拳击魔盒的效果是扣掉对方一滴血
            case "box2":
                {
                    hurt(from,to);
                }
                break;
            // 便便魔盒的效果是
            case "box3":
                {
                    // 没想好
                    hurt(from, to);
                }
                break;
            // 小丑装饰的效果是给自己回血
            case "collision":
            case "collision2":
                {
                    heal(from);
                }
                break;
            // 炸弹的效果是让对方远离自己
            case "boom":
                scare(from, to);
                break;
            default: break;
        }
    }

    // 让对手远离自己
    private void scare(PlayerController from, PlayerController to)
    {
        var dir = from.transform.position - to.transform.position;
        dir = dir.normalized;
        to.rigidBody.AddForce(dir * to.horizontalSpeed * 20, ForceMode.Force);
        Debug.Log("玩家"+from.name+"让"+to.name+"远离自己");
    }
    // 让对手靠近自己
    private void attract(PlayerController from, PlayerController to)
    {
        var dir = to.transform.position - from.transform.position;
        dir = dir.normalized;
        to.rigidBody.AddForce(dir * to.horizontalSpeed * 20, ForceMode.Force);
        Debug.Log("玩家" + from.name + "让" + to.name + "靠近自己");
    }
    // 给自己回一滴血
    private void heal(PlayerController player)
    {
        player.AddCrate();
        Debug.Log("玩家" + player.name + "给自己回了一滴血");
    }
    // 扣掉对方一滴血
    private void hurt(PlayerController from, PlayerController to)
    {
        to.RemoveCrate();
        Debug.Log("玩家" + from.name + "给"+to.name+"扣掉一滴血");
    }
}
