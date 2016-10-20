using UnityEngine;
using System.Collections;

public class InvincibilityMode : MonoBehaviour {

	void Awake () {
        DontDestroyOnLoad(transform.gameObject);
    }
}
