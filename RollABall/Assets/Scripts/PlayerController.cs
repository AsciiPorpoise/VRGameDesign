using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;
    public float speed;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

	void Update () {
	
	}

    void FixedUpdate ()
    {
        float my = Input.GetAxis("Vertical");
        float mx = Input.GetAxis("Horizontal");

        rb.AddForce(new Vector3(mx*speed, 0.0f, my*speed));
    }
}
