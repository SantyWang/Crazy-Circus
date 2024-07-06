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
    public float horizontalSpeed = 1f;
    public float moveSpeed = 2f;

    private List<GameObject> allCarriedCrates = new List<GameObject>();
    private Animator animator;
    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("run", 1.0f);
        animator.SetBool("gr", true);
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(moveLeftKey))
        {
            rigidBody.AddForce(transform.right * -horizontalSpeed, ForceMode.Force);
        }
        else if (Input.GetKey(moveRightKey))
        {
            rigidBody.AddForce(transform.right * horizontalSpeed, ForceMode.Force);
        }
        animator.SetFloat("turn", -rigidBody.velocity.z);
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
}
