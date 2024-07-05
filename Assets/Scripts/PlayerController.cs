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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(moveLeftKey))
        {
            transform.localPosition += new Vector3(0, 0, horizontalSpeed * Time.deltaTime);

            animator.SetInteger("Move Direction", 1);
        }
        else if (Input.GetKey(moveRightKey))
        {
            transform.localPosition += new Vector3(0, 0, -horizontalSpeed * Time.deltaTime);

            animator.SetInteger("Move Direction", 2);
        } else
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Crate")
        {
            GameObject.Destroy(other.gameObject);
            AddCrate();
        }
    }
}
