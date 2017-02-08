 using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Ctrl : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        Network.sendRate = 29;
	}
	
	// Update is called once per frame
	void Update () {
        if(!isLocalPlayer)
        {
            return;
        }
        transform.rotation = Input.gyro.attitude;
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
