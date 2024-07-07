using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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
    public GameObject collisonVFX;
    public GameObject boxVFX;
    public GameObject magnetVFX;
    public GameObject boomVFX;

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

    void endGame()
    {
        animator.SetBool("fail", true);
        GameObject.Find("GameMode").GetComponent<GameMode>().StopGame();
    }

    private bool disableUserInput = false;
    public void disableInput()
    {
        disableUserInput = true;
    }

    void Update()
    {
        if (disableUserInput)
        {
            return;
        }
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
            rigidBody.AddForce(transform.right * -horizontalSpeed * rigidBody.mass , ForceMode.Force);
            //单靠力量移动会很缓慢，手感不好，所以在此基础上加一个位置移动
            //transform.position += transform.right * -horizontalSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(moveRightKey) && grounded)
        {
            rigidBody.AddForce(transform.right * horizontalSpeed * rigidBody.mass, ForceMode.Force);
            //transform.position += transform.right * horizontalSpeed * Time.deltaTime;
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
                string grab = popGrab();
                if(grab != null)
                {
                    GameObject.Find("Grab Spawner").GetComponent<GrabSpawner>().useGrab(
                        this, anotherPlayer, grab
                    );
                }
            }


        }
    }

    public void OnDestroy()
    {
        // 销毁所有箱子
        foreach (GameObject crate in allCarriedCrates)
        {
            GameObject.Destroy(crate);
        }
        // 销毁所有道具
        grablist.Clear();
    }

    public void RemoveCrate()
    {
        GameObject crate = allCarriedCrates.Last();
        Destroy(crate.GetComponent<ConfigurableJoint>());
        var crateRigidBody = crate.GetComponent<Rigidbody>();
        crateRigidBody.useGravity = true;
        crateRigidBody.AddForceAtPosition(crate.transform.position - crate.transform.up * 0.2f, crate.transform.up * Random.Range(100f, 150f));
        allCarriedCrates.Remove(crate);
        CarriedCrateController carriedCrateController = crate.GetComponent<CarriedCrateController>();
        rigidBody.mass -= carriedCrateController.mass;
        carriedCrateController.playerController = null;
        carriedCrateController.ThrowAway();
        horizontalSpeed -= 0.1f;
        jumpForce -= 10f;
        // 如果没有箱子了，游戏结束
        if (allCarriedCrates.Count == 0)
        {
            endGame();
        }
    }

    public void AddCrate()
    {
        GameObject newCrate = GameObject.Instantiate(carriedCratePrefab);
        if (allCarriedCrates.Count == 0)
        {
            newCrate.transform.position = carriedCratePivot.position + carriedCratePivot.up * (0.4f + allCarriedCrates.Count * 0.4f);
            newCrate.transform.rotation = carriedCratePivot.rotation;
            var configurableJoint = newCrate.GetComponent<ConfigurableJoint>();
            configurableJoint.connectedBody = carriedCratePivot.GetComponent<Rigidbody>();
            configurableJoint.breakForce = 100;
        }
        else
        {
            newCrate.transform.position = allCarriedCrates.Last().transform.position + allCarriedCrates.Last().transform.up  * 0.4f;
            newCrate.transform.rotation = allCarriedCrates.Last().transform.rotation;
            var configurableJoint = newCrate.GetComponent<ConfigurableJoint>();
            configurableJoint.connectedBody = allCarriedCrates.Last().GetComponent<Rigidbody>();
            configurableJoint.breakForce = Mathf.Max(100 - allCarriedCrates.Count * 10, 30);
        }
        horizontalSpeed += 0.1f;
        allCarriedCrates.Add(newCrate);
        CarriedCrateController carriedCrateController = newCrate.GetComponent<CarriedCrateController>();
        carriedCrateController.movingSpeed = moveSpeed;
        rigidBody.mass += carriedCrateController.mass;
        carriedCrateController.playerController = this;
        jumpForce += 10f;
        rigidBody.mass += newCrate.GetComponent<CarriedCrateController>().mass;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (disableUserInput)
        {
            return;
        }
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
    public List<string> grablist = new List<string>();
    private int grabNum = 1;
    private void pushGrab(GameObject grab)
    {
        // 目前只持有一个道具
        if(grablist.Count >= grabNum)
        {
            grablist.RemoveAt(0);
        }
        grablist.Add(grab.name);
        GameObject.Find("UI").GetComponent<UIController>().UpdateGrabPanel(this);
    }

    // 获取最后一个道具
    public string popGrab()
    {
        if(grablist.Count == 0)
        {
            return null;
        }
        var last = grablist.Last();
        grablist.RemoveAt(grablist.Count - 1);
        GameObject.Find("UI").GetComponent<UIController>().UpdateGrabPanel(this);
        return last;
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
