 using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Ctrl : NetworkBehaviour {

    Animator anim;
    [SyncVar]
    public Vector3 accel;

	// Use this for initialization
	void Start () {
        Network.sendRate = 29;
        anim = gameObject.GetComponentInChildren<Animator>();
        Input.gyro.enabled = true;
        if(!isLocalPlayer)
            StartCoroutine(printAccel());
	}
	
	// Update is called once per frame
	void Update () {

        Screen.orientation = ScreenOrientation.Portrait;
        accel = Input.acceleration;

        if(!isLocalPlayer)
        {
            return;
        }

        //transform.Rotate(Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, Input.gyro.rotationRateUnbiased.z);
        transform.up = -MovAverage(accel.normalized);
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, -transform.rotation.z, transform.rotation.w);


        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-0.1f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0.1f, 0f, 0f);
        }

        if(accel.x > 1.5 || accel.y > 1.5 || accel.x < -1.5 || accel.y < -1.5)
        {
            anim.Play("Swinging");
            Debug.Log("Swinging");
        } else
        {
//            anim.Play("Entry");
        }
    }

    private int sizeFilter = 15;
    private Vector3[] filter;
    private Vector3 filterSum = Vector3.zero;
    private int posFilter = 0;
    private int qSamples = 0;

    Vector3 MovAverage(Vector3 sample)
    {
       if(qSamples == 0)
        {
            filter = new Vector3[sizeFilter];
        }
        filterSum += sample - filter[posFilter];
        filter[posFilter++] = sample;
        if(posFilter > qSamples)
        {
            qSamples = posFilter;
        }
        posFilter = posFilter % sizeFilter;
        return filterSum / qSamples;
    }

    IEnumerator printAccel()
    {
        Debug.Log(Network.natFacilitatorIP+": Accel: " + accel + " xswingl: " + (accel.x > 1.5) + " xswingr: " + (accel.x < -1.5) + " yswingl: " + (accel.y > 1.5) + " yswingr: " + (accel.y < -1.5));
        yield return new WaitForSeconds (0.5f);
        StartCoroutine(printAccel());
    }
}
