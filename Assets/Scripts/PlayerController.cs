using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static AmazingAssets.CurvedWorld.Example.RunnerPlayer;

public class PlayerController : MonoBehaviour
{

    public Transform carriedCratePivot;
    public GameObject carriedCratePrefab;

    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public float horizontalSpeed = 1f;
    public float moveSpeed = 2f;

    private List<GameObject> allCarriedCrates = new List<GameObject>();
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    bool isColliding = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(moveLeftKey))
        {

            // 如果发生了碰撞，且是player1，那么不允许朝着player2的方向移动
            if (isColliding)
            {
                GameObject player1 = GetPlayer1();
                GameObject player2 = GetPlayer2();
                if (player1 == null || player2 == null)
                {
                    return;
                }
                Vector3 player1Pos = player1.transform.position;
                Vector3 player2Pos = player2.transform.position;

                Vector3 nextDirection = new Vector3(0, 0, horizontalSpeed * Time.deltaTime);
                if (IsPlayer1(gameObject))
                {
                    // 如果移动方向和player2的方向相同，那么不允许移动
                    Vector3 direction = player2Pos - player1Pos;
                    if (Vector3.Dot(nextDirection, direction) > 0)
                    {
                        return;
                    }
                }

                if (IsPlayer2(gameObject))
                {
                    // 如果移动方向和player1的方向相同，那么不允许移动
                    Vector3 direction = player1Pos - player2Pos;
                    if (Vector3.Dot(nextDirection, direction) > 0)
                    {
                        return;
                    }
                }

            }
            transform.localPosition += new Vector3(0, 0, horizontalSpeed * Time.deltaTime);
            animator.SetInteger("Move Direction", 1);
        }
        else if (Input.GetKey(moveRightKey))
        {

            if (isColliding)
            {
                GameObject player1 = GetPlayer1();
                GameObject player2 = GetPlayer2();
                if (player1 == null || player2 == null)
                {
                    return;
                }
                Vector3 nextDirection = new Vector3(0, 0, -horizontalSpeed * Time.deltaTime);
                
                Vector3 player1Pos = player1.transform.position;
                Vector3 player2Pos = player2.transform.position;
                if (IsPlayer1(gameObject))
                {
                    // 如果移动方向和player2的方向相同，那么不允许移动
                    Vector3 direction = player2Pos - player1Pos;
                    if (Vector3.Dot(nextDirection, direction) > 0)
                    {
                        return;
                    }
                }

                if (IsPlayer2(gameObject))
                {
                    // 如果移动方向和player1的方向相同，那么不允许移动
                    Vector3 direction = player1Pos - player2Pos;
                    if (Vector3.Dot(nextDirection, direction) > 0)
                    {
                        return;
                    }
                }

            }
            transform.localPosition += new Vector3(0, 0, -horizontalSpeed * Time.deltaTime);

            animator.SetInteger("Move Direction", 2);
        }
        else
        {
            animator.SetInteger("Move Direction", 0);
        }
    }

    private void AddCrate()
    {
        GameObject newCrate = GameObject.Instantiate(carriedCratePrefab, carriedCratePivot);
        newCrate.transform.localPosition = new Vector3(0, allCarriedCrates.Count * 0.398f, 0);
        allCarriedCrates.Add(newCrate);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isColliding = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Crate")
        {
            GameObject.Destroy(other.gameObject);
            AddCrate();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isColliding = true;
        }
    }


    private bool IsPlayer1(GameObject go)
    {
        return go.name.IndexOf("Player1") > -1;
    }

    private bool IsPlayer2(GameObject go)
    {
        return go.name.IndexOf("Player2") > -1;
    }

    private GameObject GetPlayer1()
    {
        return GameObject.FindGameObjectsWithTag("Player").Where(IsPlayer1).FirstOrDefault();
    }

    private GameObject GetPlayer2()
    {
        return GameObject.FindGameObjectsWithTag("Player").Where(IsPlayer2).FirstOrDefault();
    }
}
