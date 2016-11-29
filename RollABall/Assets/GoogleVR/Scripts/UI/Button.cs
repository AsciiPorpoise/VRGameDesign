using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using ProgressBar;
using System.Linq;

public class Button : MonoBehaviour, IGvrGazeResponder {

    float timeSum = 0f;
    bool looking = false;

    public void OnGazeExit()
    {
        looking = false;
    }

    public void OnGazeTrigger()
    {
        SceneManager.LoadScene("Level1");
    }

    void IGvrGazeResponder.OnGazeEnter()
    {
        looking = true;
    }

    // Use this for initialization
    void OnGazeEnter()
    {
    }

    void Update()
    {
        if(looking)
        {
            timeSum += Time.deltaTime;
        } else
        {
            timeSum = 0;
        }

        GameObject.FindGameObjectsWithTag("Loading")[0].GetComponent<TextMesh>().text = new String('●', ((int)(timeSum*10f)));

        if (timeSum >= 2)
        {
            OnGazeTrigger();
        }
    }
}
