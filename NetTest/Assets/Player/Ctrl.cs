 using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Ctrl : NetworkBehaviour {

    Animator anim;

	// Use this for initialization
	void Start () {
        Network.sendRate = 29;
        StartCoroutine(printAccel());
        anim = gameObject.GetComponentInChildren<Animator>();
        Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {

        Screen.orientation = ScreenOrientation.Portrait;

        if(!isLocalPlayer)
        {
            return;
        }

        //transform.Rotate(Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, Input.gyro.rotationRateUnbiased.z);
        transform.up = -MovAverage(Input.acceleration.normalized);
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, -transform.rotation.z, transform.rotation.w);

        /*
        Debug.Log("Gyro: "+SystemInfo.supportsGyroscope);
        Debug.Log("Gyro: "+Input.gyro.enabled);
        Debug.Log(Input.gyro.attitude.eulerAngles);
        Debug.Log("GyroAccel: "+Input.gyro.userAcceleration);
        */

        //transform.rotation = new Quaternion(Input.acceleration.x, Input.acceleration.y, Input.acceleration.z, 0f);

        //transform.rotation = Quaternion.Euler(-Input.gyro.attitude.eulerAngles.x, -Input.gyro.attitude.eulerAngles.y, Input.gyro.attitude.eulerAngles.z);//TODO: Map gyro by axis
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-0.1f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0.1f, 0f, 0f);
        }

        if(Input.acceleration.x > 1.5 || Input.acceleration.y > 1.5 || Input.acceleration.x < -1.5 || Input.acceleration.y < -1.5)
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
        Debug.Log("Accel: " + Input.acceleration + " xswingl: " + (Input.acceleration.x > 1.5) + " xswingr: " + (Input.acceleration.x < -1.5) + " yswingl: " + (Input.acceleration.y > 1.5) + " yswingr: " + (Input.acceleration.y < -1.5));
        yield return new WaitForSeconds (0.5f);
        StartCoroutine(printAccel());
    }
}
