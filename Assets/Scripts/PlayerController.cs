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

    void endGame()
    {
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
            //���������ƶ���ܻ������ָв��ã������ڴ˻����ϼ�һ��λ���ƶ�
            transform.position += transform.right * -horizontalSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(moveRightKey) && grounded)
        {
            rigidBody.AddForce(transform.right * horizontalSpeed * rigidBody.mass, ForceMode.Force);
            transform.position += transform.right * horizontalSpeed * Time.deltaTime;
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
        // ������������
        foreach (GameObject crate in allCarriedCrates)
        {
            GameObject.Destroy(crate);
        }
        // �������е���
        grablist.Clear();
    }

    public void RemoveCrate()
    {
        GameObject crate = allCarriedCrates.Last();
        crate.transform.localPosition = new Vector3(0, 0, 0);
        allCarriedCrates.Remove(crate);
        GameObject.Destroy(crate);
        rigidBody.mass -= crate.GetComponent<CarriedCrateController>().mass;
        // ���û�������ˣ���Ϸ����
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

    public void OnPlayerJointBreak(float breakForce)
    {
        // ���ӶϿ�����Ϸ����
        endGame();
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
            // �����õ���
            pushGrab(other.gameObject);
            GameObject.Find("AudioSystem").GetComponent<AudioSystem>().PlayProp();
            GameObject.Destroy(other.gameObject);
        }
    }

    // ���е���
    public List<string> grablist = new List<string>();
    private int grabNum = 1;
    private void pushGrab(GameObject grab)
    {
        // Ŀǰֻ����һ������
        if(grablist.Count >= grabNum)
        {
            grablist.RemoveAt(0);
        }
        grablist.Add(grab.name);
        GameObject.Find("UI").GetComponent<UIController>().UpdateGrabPanel(this);
    }

    // ��ȡ���һ������
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
