using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	
	void Update () {
        transform.Rotate(new Vector3(10f, 10f, 10f) * Time.deltaTime);
	}
}
