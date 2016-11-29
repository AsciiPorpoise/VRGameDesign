using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
