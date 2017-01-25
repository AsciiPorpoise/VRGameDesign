using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;
    public float speed;
    public GameObject death;

    private Vector3 rinit;
    private Vector3 calib;
    private bool jump;
    private bool levelLoading;


    private int score;
    private float dscore;
    private TextMesh scoreLabel;
    private TextMesh scoreLabelShadow;

    private static int currentLevel = 0;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rinit = transform.position;
        calib = 4*Input.acceleration;
        jump = false;
        levelLoading = false;
        dscore = 0;
        score = 0;
        Debug.Log("Level: " + currentLevel);
    }

    void Update () {
        dscore += Time.deltaTime;
        if(dscore >=1f)
        {
            dscore -= 1f;
            score -= 10;
            UpdateScore();
        }

    }

    void FixedUpdate()
    {
        float my = Input.GetAxis("Vertical") - Input.acceleration.z*4 + calib.z;
        float mx = Input.GetAxis("Horizontal") + Input.acceleration.x*4 - calib.x;

        rb.AddForce(new Vector3(mx * speed, 0.0f, my * speed));
        if (Input.GetKeyDown(KeyCode.Space) && !jump)
        {
            rb.AddForce(new Vector3(0f, 35f, 0f), ForceMode.VelocityChange);
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
        if (target.gameObject.CompareTag("LevelEnd") && !levelLoading)
        {
            Debug.Log("Hit level end!");
            levelLoading = true;
            target.gameObject.GetComponent<PickupController>().Die();
            BumpScore();
            currentLevel++;
            Die();
            Debug.Log("Loading level " + currentLevel);
            Invoke("loadQueued", 2);
            //SceneManager.LoadScene("Level" + currentLevel);
        }
        else if (target.gameObject.CompareTag("Pickup"))
        {
            target.gameObject.GetComponent<PickupController>().Die();
            BumpScore();
        }
        else if(target.gameObject.CompareTag("Respawn"))
        {
            transform.position = rinit;
            transform.rotation = Quaternion.identity;
            rb.velocity = new Vector3();
            rb.angularVelocity = new Vector3();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        jump = false;
    }

    public void loadQueued()
    {
        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void BumpScore()
    {
        this.score += 100;
        UpdateScore();
    }

    public void UpdateScore() {
        if (scoreLabel == null)
        {
           if(GameObject.Find("ScoreLabel") != null)
            {
                scoreLabel = GameObject.Find("ScoreLabel").GetComponent<TextMesh>();
                scoreLabelShadow = GameObject.Find("ScoreLabelShadow").GetComponent<TextMesh>();
            }
        } else
        {
            scoreLabel.text = "Score: " + this.score.ToString();
            scoreLabelShadow.text = "Score: " + this.score.ToString();
        }
    }

    public void Die()
    {
        GameObject.Instantiate(death, transform.position, transform.rotation);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        rb.isKinematic = true;
        //Destroy(gameObject);
    }
}

