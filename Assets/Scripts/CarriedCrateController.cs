using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarriedCrateController : MonoBehaviour
{

    public float mass = 0.1f;
    [HideInInspector]
    public PlayerController playerController;
    [HideInInspector]
    public float movingSpeed;
    Rigidbody rigidBody;
    bool isThrow = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!isThrow)
        {
            return;
        }
        rigidBody.MovePosition(transform.position + Vector3.left * movingSpeed * Time.deltaTime);

        if (transform.position.y < -300)
        {
            Destroy(this.gameObject);
        }
    }

    void OnJointBreak(float breakForce)
    {
        // 需要知道所在的player
        Debug.Log("Crate is broken");
        if (playerController == null)
        {
            return;
        }
        var index = playerController.allCarriedCrates.IndexOf(gameObject);
        if (index >= 0)
        {
            for (int i = playerController.allCarriedCrates.Count; i > index; i--)
            {
                playerController.RemoveCrate();
            }
        }

    }

    public void ThrowAway ()
    {
        isThrow = true;
    }
}
