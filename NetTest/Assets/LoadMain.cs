using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadMain : MonoBehaviour {
	void Start () {
        if(SceneManager.GetSceneByName("Main").isLoaded)
        {
            Destroy(GameObject.Find("Net"));
            SceneManager.UnloadScene("Main");
        }
        StartCoroutine(Load());
	}

    IEnumerator Load()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Main");
    }

}
