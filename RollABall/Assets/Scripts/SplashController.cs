using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class SplashController : MonoBehaviour {

    public int countdown;
    public float interval;
    public string sceneToLoad;
    public bool startOnAwake;

    public SplashController(int countdown, float interval, string sceneToLoad)
    {
        this.countdown = countdown;
        this.interval = interval;
        this.sceneToLoad = sceneToLoad;
    }

	// Use this for initialization
	void Start () {
        if(startOnAwake)
        {
            Begin();
        }
    }

    public void Begin()
    {
        Loop();
    }

    void Loop()
    {
        if(countdown <= 0)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        gameObject.GetComponent<TextMesh>().text = countdown + "...";
        countdown--;
        Invoke("Loop", interval);
    }
}
