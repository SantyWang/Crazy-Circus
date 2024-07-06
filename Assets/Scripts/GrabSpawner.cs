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
        // ���name����(cloned) Ҫȥ��
        name = name.Replace("(Clone)", "");

        switch (name)
        {
            // �㽶��Ч���Ǹ��Է����Լ��ķ������
            case "banana1":
            case "banana2":
                attract(from, to);
                break;
            // ����ħ�е�Ч�����öԷ�ҡ�η�������
            case "box1":
                {
                    // û���
                    hurt(from, to);
                }
                break;
            // ȭ��ħ�е�Ч���ǿ۵��Է�һ��Ѫ
            case "box2":
                {
                    hurt(from,to);
                }
                break;
            // ���ħ�е�Ч����
            case "box3":
                {
                    // û���
                    hurt(from, to);
                }
                break;
            // С��װ�ε�Ч���Ǹ��Լ���Ѫ
            case "collision":
            case "collision2":
                {
                    heal(from);
                }
                break;
            // ը����Ч�����öԷ�Զ���Լ�
            case "boom":
                scare(from, to);
                break;
            default: break;
        }
    }

    // �ö���Զ���Լ�
    private void scare(PlayerController from, PlayerController to)
    {
        var dir = from.transform.position - to.transform.position;
        dir = dir.normalized;
        to.rigidBody.AddForce(dir * to.horizontalSpeed * 20, ForceMode.Force);
        Debug.Log("���"+from.name+"��"+to.name+"Զ���Լ�");
    }
    // �ö��ֿ����Լ�
    private void attract(PlayerController from, PlayerController to)
    {
        var dir = to.transform.position - from.transform.position;
        dir = dir.normalized;
        to.rigidBody.AddForce(dir * to.horizontalSpeed * 20, ForceMode.Force);
        Debug.Log("���" + from.name + "��" + to.name + "�����Լ�");
    }
    // ���Լ���һ��Ѫ
    private void heal(PlayerController player)
    {
        player.AddCrate();
        Debug.Log("���" + player.name + "���Լ�����һ��Ѫ");
    }
    // �۵��Է�һ��Ѫ
    private void hurt(PlayerController from, PlayerController to)
    {
        to.RemoveCrate();
        Debug.Log("���" + from.name + "��"+to.name+"�۵�һ��Ѫ");
    }
}
