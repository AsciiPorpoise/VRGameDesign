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
        //Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {

        Screen.orientation = ScreenOrientation.Portrait;

        if(!isLocalPlayer)
        {
            return;
        }
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

        if(System.Math.Abs(Input.acceleration.x) > 1.5 || System.Math.Abs(Input.acceleration.y) > 1.5)
        {
            anim.Play("Swinging");
        } else
        {
            anim.Play("Entry");
        }
    }

    IEnumerator printAccel()
    {
        Debug.Log("Accel: " + Input.acceleration + " xswingl: " + (Input.acceleration.x > 1.5) + " xswingr: " + (Input.acceleration.x < -1.5) + " yswingl: " + (Input.acceleration.y > 1.5) + " yswingr: " + (Input.acceleration.y < -1.5));
        yield return new WaitForSeconds (0.5f);
        StartCoroutine(printAccel());
    }
}
