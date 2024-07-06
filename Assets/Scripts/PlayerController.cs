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
    public GameObject footStepVFX;
    public Transform leftFootAnchor;
    public Transform rightFootAnchor;
    public AudioClip leftFootAudio;
    public AudioClip rightFootAudio;
    [HideInInspector]
    public List<GameObject> allCarriedCrates = new List<GameObject>();
    private Animator animator;
    private AudioSource audioSource;
    Rigidbody rigidBody;
    bool grounded = false;

    public KeyCode grabKey1 = KeyCode.Alpha1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("run", 1.0f);
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
        if (Input.GetKey(moveLeftKey) && grounded)
        {
            rigidBody.AddForce(transform.right * -horizontalSpeed * rigidBody.mass, ForceMode.Force);
        }
        else if (Input.GetKey(moveRightKey) && grounded)
        {
            rigidBody.AddForce(transform.right * horizontalSpeed * rigidBody.mass, ForceMode.Force);
        }
        animator.SetFloat("turn", -rigidBody.velocity.z);
        if (Input.GetKey(jump) && grounded)
        {
            animator.SetBool("jump", true);
        }
        else
        {
            animator.SetBool("jump", false);
        }

        if (Input.GetKeyUp(grabKey1))
        {
            string[] grabs = getGrabs();
            Debug.Log("Player name:" + gameObject.name + "; Grabs:[" + grabs[0] + "," + grabs[1] + "," + grabs[2] + "]");

        }
    }

    private void AddCrate()
    {
        GameObject newCrate = GameObject.Instantiate(carriedCratePrefab);
        if (allCarriedCrates.Count == 0)
        {
            newCrate.transform.position = carriedCratePivot.position + carriedCratePivot.up * (0.4f + allCarriedCrates.Count * 0.4f);
            newCrate.transform.rotation = carriedCratePivot.rotation;
            newCrate.GetComponent<ConfigurableJoint>().connectedBody = carriedCratePivot.GetComponent<Rigidbody>();
        }
        else
        {
            newCrate.transform.position = allCarriedCrates.Last().transform.position + allCarriedCrates.Last().transform.up  * 0.4f;
            newCrate.transform.rotation = allCarriedCrates.Last().transform.rotation;
            newCrate.GetComponent<ConfigurableJoint>().connectedBody = allCarriedCrates.Last().GetComponent<Rigidbody>();
        }
        allCarriedCrates.Add(newCrate);
        rigidBody.mass += newCrate.GetComponent<CarriedCrateController>().mass;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Crate")
        {
            GameObject.Destroy(other.gameObject);
            AddCrate();
            GameObject.Find("AudioSystem").GetComponent<AudioSystem>().PlayProp();
        }
        if(other.gameObject.tag == "Grab")
        {
            GameObject.Destroy(other.gameObject);
            // 人物获得道具
            addGrab(other.gameObject);
            GameObject.Find("AudioSystem").GetComponent<AudioSystem>().PlayProp();
        }
    }

    // 持有道具
    private string[] grablist = new string[3];
    private void addGrab(GameObject grab)
    {
        // 只能有3个道具，当道具数量大于3个时，弹出最早的道具
        if (grablist[2] != null)
        {
            grablist[0] = grablist[1];
            grablist[1] = grablist[2];
            grablist[2] = grab.name;
        }
        else if (grablist[1] != null)
        {
            grablist[2] = grab.name;
        }
        else if (grablist[0] != null)
        {
            grablist[1] = grab.name;
        }
        else
        {
            grablist[0] = grab.name;
        }
        Debug.Log("Player name:" + gameObject.name + "; Grabs:[" + grablist[0] + "," + grablist[1] + "," + grablist[2] + "]");
    }

    public string[] getGrabs()
    {
        return grablist;
    }

    void Jump()
    {
        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Force);
    }

    public void PlayLeftFootVFX ()
    {
        var effect = GameObject.Instantiate(footStepVFX);
        effect.transform.position = leftFootAnchor.position;
        audioSource.PlayOneShot(leftFootAudio, Random.Range(0.8f, 1.2f));
    }

    public void PlayRightFootVFX()
    {
        var effect = GameObject.Instantiate(footStepVFX);
        effect.transform.position = rightFootAnchor.position;
        audioSource.PlayOneShot(rightFootAudio, Random.Range(0.8f, 1.2f));
    }
} 
