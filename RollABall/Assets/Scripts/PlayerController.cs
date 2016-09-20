using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;
    private Vector3 rinit;
    public float speed;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rinit = transform.position;
    }

	void Update () {
	
	}

    void FixedUpdate ()
    {
        float my = Input.GetAxis("Vertical");
        float mx = Input.GetAxis("Horizontal");

        rb.AddForce(new Vector3(mx*speed, 0.0f, my*speed));
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
            transform.position = rinit;
        }
    }

    void OnTriggerEnter(Collider target)
    {
        Destroy(target.gameObject);
    }
}
