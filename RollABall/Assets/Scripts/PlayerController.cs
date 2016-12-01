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


    private int score;
    private float dscore;
    private TextMesh scoreLabel;
    private TextMesh scoreLabelShadow;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rinit = transform.position;
        calib = 4*Input.acceleration;
        jump = false;
        dscore = 0;
        score = 0;
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
            rb.AddForce(new Vector3(0f, 30f, 0f), ForceMode.VelocityChange);
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
        if (target.gameObject.CompareTag("LevelEnd"))
        {
            target.gameObject.GetComponent<PickupController>().Die();
            BumpScore();
            SceneManager.LoadScene("Level2");
            Die();

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

    public void BumpScore()
    {
        this.score += 100;
    }

    public void UpdateScore() {
        if (scoreLabel == null)
        {
            scoreLabel = GameObject.Find("ScoreLabel").GetComponent<TextMesh>();
            scoreLabelShadow = GameObject.Find("ScoreLabelShadow").GetComponent<TextMesh>();
        }
        scoreLabel.text = this.score.ToString();
        scoreLabelShadow.text = this.score.ToString();
    }

    public void Die()
    {
        GameObject.Instantiate(death, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

