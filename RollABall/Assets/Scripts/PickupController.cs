using UnityEngine;
using System.Collections;


public class PickupController : MonoBehaviour {

    public GameObject death;
    public float rs = 10f;

    private int count = 0;

	// Use this for initialization
	void Start () {
        count++;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(rs, rs, rs) * Time.deltaTime);
    }

    public void Die()
    {
        GameObject.Instantiate(death, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
