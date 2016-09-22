using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;
    public float speed;

    private Vector3 rinit;
    private bool jump;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rinit = transform.position;
        jump = false;
    }

    void Update () {
	
	}

    void FixedUpdate()
    {
        float my = Input.GetAxis("Vertical");
        float mx = Input.GetAxis("Horizontal");

        rb.AddForce(new Vector3(mx * speed, 0.0f, my * speed));
        if (Input.GetKeyDown(KeyCode.Space) && !jump)
        {
            rb.AddForce(new Vector3(0f, 15f, 0f), ForceMode.VelocityChange);
            rb.AddForce(new Vector3(0f, -100f, 0f), ForceMode.Acceleration);
            // rb.AddExplosionForce(10f, transform.position + new Vector3(0f, -1f, 0f), 10f);
            jump = true;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
            transform.position = rinit;
        }
    }


    void OnTriggerEnter(Collider target)
    {
        Debug.Log(target.gameObject.GetType());
        if(target.gameObject)
        Destroy(target.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        jump = false;
    }
}
