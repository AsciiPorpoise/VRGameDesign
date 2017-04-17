 using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {

    Animator anim;
    public GameObject bulletPrefab;
    public GameObject bulletDeathPrefab;

    [SyncVar]
    public Vector3 accel;

    private int playerNum;

	// Use this for initialization
	void Start () {
        // Set playerNum for local or remote player to ensure correct starting pos
        playerNum = isLocalPlayer ? GameController.playerNum : GameController.playerNum == 1 ? 2 : 1;
        // Set starting pos
        transform.position = GameObject.Find("Player" + playerNum + " Spawn").transform.position;
        transform.Rotate(0f, playerNum == 2 ? 180f : 0f, 0f);

        anim = gameObject.GetComponentInChildren<Animator>();
        Input.gyro.enabled = true;
        //if(!isLocalPlayer)
        //    StartCoroutine(printAccel());
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
        transform.rotation = new Quaternion(playerNum == 1 ? transform.rotation.x : -transform.rotation.x,
            playerNum == 1 ? transform.rotation.y : -transform.rotation.y,
            playerNum == 1 ? -transform.rotation.z : transform.rotation.z, 
            transform.rotation.w);


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

    public void LaunchProjectile(int spawn)
    {
        if (!isLocalPlayer)
        {
            return;
        }
        var spawner = GameObject.Find("P" + GameController.playerNum + "Spawn" + spawn);
        Debug.Log(spawner);
        CmdLaunchProjectile(spawner.transform.position, spawner.transform.forward);
    }

    [Command]
    void CmdLaunchProjectile(Vector3 pos, Vector3 forward)
    {
   
        // create the bullet object from the bullet prefab
        GameObject bullet = (GameObject)Instantiate(
            bulletPrefab,
            pos - forward,
            Quaternion.identity);

        // make the bullet move away in front of the player
        bullet.GetComponent<Rigidbody>().velocity = forward * 25;

        NetworkServer.Spawn(bullet);

        // make bullet disappear after 2 seconds
        Destroy(bullet, 2.0f);
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
        yield return new WaitForSeconds (0.5f);
        StartCoroutine(printAccel());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetType().IsAssignableFrom(bulletPrefab.GetType())) 
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
            collision.gameObject.GetComponent<Collider>().enabled = false;
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
            //TODO Find a better way to do this. There must be a non-deprecated way to manipulate the Particle System
            collision.gameObject.GetComponent<ParticleSystem>().startSpeed = 8;
            collision.gameObject.GetComponent<ParticleSystem>().gravityModifier = 1;
            collision.gameObject.GetComponent<ParticleSystem>().emissionRate = 100;
            Destroy(collision.gameObject, 0.2f);
        } else
        {
            Debug.Log(collision.gameObject);
        }
    }
}
