using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public bool disableVR;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;
        if(disableVR)
        {
            GameObject.Destroy(GameObject.Find("GvrViewerMain"));
            GameObject.Destroy(GameObject.Find("GvrControllerMain"));
        }
	}
	
    void Update()
    {
        transform.position = player.transform.position + offset;
    }

	// Update is called once per frame
	void LateUpdate () {
        
	}   
}
