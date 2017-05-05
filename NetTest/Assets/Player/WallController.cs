using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {

    public GameObject bulletPrefab;

    int score = 0;

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
            score++;
        }
        else
        {
            Debug.Log(collision.gameObject);
        }
    }
}
