using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarriedCrateController : MonoBehaviour
{

    public float mass = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnJointBreak(float breakForce)
    {
        // 需要知道所在的player
        Debug.Log("Crate is broken");
        var root = this.GetComponent<ConfigurableJoint>().connectedBody.transform.parent.root.GetComponent<PlayerController>();
        root.OnPlayerJointBreak(breakForce);
    }
}
