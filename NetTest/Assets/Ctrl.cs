 using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Ctrl : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        Network.sendRate = 29;
        Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        if(!isLocalPlayer)
        {
            return;
        }

        Debug.Log("Gyro: "+SystemInfo.supportsGyroscope);
        Debug.Log("Gyro: "+Input.gyro.enabled);
        Debug.Log(Input.gyro.attitude);

        transform.rotation = Input.gyddadadro.attitude;//TODO: Map gyro by axis
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-0.1f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0.1f, 0f, 0f);
        }
    }
}
