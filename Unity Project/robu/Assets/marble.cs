using UnityEngine;
using System.Collections;

public class marble : MonoBehaviour {

    public Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

void Update () {
	    if (Input.GetKeyDown("w"))
        {
            rb.AddForce(Vector3.forward, ForceMode.VelocityChange);
        }
        if (Input.GetKeyDown("a"))
        {
            rb.AddForce(-Vector3.right, ForceMode.VelocityChange);
        }
        if (Input.GetKeyDown("s"))
        {
            rb.AddForce(-Vector3.forward, ForceMode.VelocityChange);
        }
        if (Input.GetKeyDown("d"))
        {
            rb.AddForce(Vector3.right, ForceMode.VelocityChange);
        }
    }
}
