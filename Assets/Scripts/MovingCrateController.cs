using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCrateController : MonoBehaviour
{
    public Vector3 moveDirection = new Vector3(1, 0, 0);    //Set by spawner after instantiating
    public float movingSpeed = 1;                           //Set by spawner after instantiating

    Rigidbody rigidBody;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rigidBody.MovePosition(transform.position + moveDirection * movingSpeed * Time.deltaTime);

        if (transform.position.y < -300)
        {
            Destroy(this.gameObject);
        }
    }
}
