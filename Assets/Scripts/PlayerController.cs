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
    public KeyCode grabKey = KeyCode.S;
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
    private AudioSystem audioSystem;
    public Rigidbody rigidBody;
    bool grounded = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("run", 1.0f);
        rigidBody = GetComponent<Rigidbody>();
        audioSystem = GameObject.Find("AudioSystem").GetComponent<AudioSystem>();
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

        if (Input.GetKeyUp(grabKey))
        {
            PlayerController anotherPlayer = getAnotherPlayer();
            
            if (anotherPlayer !=null)
            {
                string grab = shiftGrab();
                if(grab != null)
                {
                    GameObject.Find("Grab Spawner").GetComponent<GrabSpawner>().useGrab(
                        this, anotherPlayer, grab
                    );
                }
            }


        }
    }
    public void RemoveCrate()
    {
        GameObject crate = allCarriedCrates.Last();
        crate.transform.localPosition = new Vector3(0, 0, 0);
        allCarriedCrates.Remove(crate);
        GameObject.Destroy(crate);
        rigidBody.mass -= crate.GetComponent<CarriedCrateController>().mass;
    }

    public void AddCrate()
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
        if (other.gameObject.tag == "Grab")
        {
            // 人物获得道具
            pushGrab(other.gameObject);
            GameObject.Find("AudioSystem").GetComponent<AudioSystem>().PlayProp();
            GameObject.Destroy(other.gameObject);
        }
    }

    // 持有道具
    private string[] grablist = new string[3];
    private int grabNum = 3;
    private void pushGrab(GameObject grab)
    {
        string[] res = new string[grabNum];
        res[0] = grab.name;
        // push 压入一个道具,最先进入的道具在最后，超出长度的道具被丢弃
        for (int i = 1; i < grabNum; i++)
        {
            res[i] = grablist[i - 1];
        }
        grablist = res;
    }

    // 获取最后一个道具
    public string shiftGrab()
    {
        string[] res = new string[grabNum];
        if (grablist[0] == null)
        {
            return null;
        }
        string first = grablist[0];
        for (int i = 0; i < grabNum-1 ; i++)
        {
            res[i] = grablist[i+1];
        }
        grablist = res;
        return first;
    }

    private PlayerController getAnotherPlayer()
    {
        GameObject[] playersGo = GameObject.FindGameObjectsWithTag("Player");
        PlayerController anotherPlayer = null;
        foreach (GameObject playerGo in playersGo)
        {
            if (playerGo != gameObject)
            {
                anotherPlayer = playerGo.GetComponent<PlayerController>();
            }
        }
        return anotherPlayer;
    }

    void Jump()
    {
        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Force);
        audioSystem.PlayJump();
    }

    public void PlayLeftFootVFX()
    {
        var effect = GameObject.Instantiate(footStepVFX);
        effect.transform.position = leftFootAnchor.position;
        audioSystem.PlayFootLeft();
    }

    public void PlayRightFootVFX()
    {
        var effect = GameObject.Instantiate(footStepVFX);
        effect.transform.position = rightFootAnchor.position;
        audioSystem.PlayFootRight();
    }
}
