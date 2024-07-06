using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform carriedCratePivot;
    public GameObject carriedCratePrefab;

    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode jump = KeyCode.Space;
    public float horizontalSpeed = 1f;
    public float moveSpeed = 2f;
    public int initialCrateNum = 3;
    public float jumpForce = 5.0f;

    private List<GameObject> allCarriedCrates = new List<GameObject>();
    private Animator animator;
    Rigidbody rigidBody;
    bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("run", 1.0f);
        rigidBody = GetComponent<Rigidbody>();
        for (int i = 0; i < initialCrateNum; i++)
        {
            AddCrate();
        }
    }

    void Update()
    {
        //GROUNDED
        if (Physics.Raycast(transform.position + new Vector3(0.1f, 0.1f, 0.1f), Vector3.down, 0.15f)
          || Physics.Raycast(transform.position + new Vector3(0.1f, 0.1f, -0.1f), Vector3.down, 0.15f)
          || Physics.Raycast(transform.position + new Vector3(0f, 0.1f, 0f), Vector3.down, 0.15f)
          || Physics.Raycast(transform.position + new Vector3(-0.1f, 0.1f, -0.1f), Vector3.down, 0.15f)
          || Physics.Raycast(transform.position + new Vector3(-0.1f, 0.1f, 0.1f), Vector3.down, 0.15f))
        {
            grounded = true;
            animator.SetBool("gr", true);
        }
        else
        {
            grounded = false;
            animator.SetBool("gr", false);
        }
        if (Input.GetKey(moveLeftKey))
        {
            rigidBody.AddForce(transform.right * -horizontalSpeed, ForceMode.Force);
        }
        else if (Input.GetKey(moveRightKey))
        {
            rigidBody.AddForce(transform.right * horizontalSpeed, ForceMode.Force);
        }
        animator.SetFloat("turn", -rigidBody.velocity.z);
        //JUMP
        if (Input.GetKey(jump) && grounded)
        {
            animator.SetBool("jump", true);
        }
        else
        {
            animator.SetBool("jump", false);
        }
    }

    private void AddCrate()
    {
        GameObject newCrate = GameObject.Instantiate(carriedCratePrefab, carriedCratePivot);
        newCrate.transform.localPosition = new Vector3(0, allCarriedCrates.Count * 0.398f, 0);
        allCarriedCrates.Add(newCrate);
        rigidBody.mass += newCrate.GetComponent<CarriedCrateController>().mass;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Crate")
        {
            GameObject.Destroy(other.gameObject);
            AddCrate();
        }
    }

    void Jump()
    {
        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Force);
    }
} 
