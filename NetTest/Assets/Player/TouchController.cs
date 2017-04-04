using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var playerCtrl = gameObject.GetComponent<PlayerController>();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerCtrl.CmdLaunchProjectile(3);
        }
        else if (Input.GetKeyDown(KeyCode.A)) {
            playerCtrl.CmdLaunchProjectile(2);
        }
        else if (Input.GetKeyDown(KeyCode.Z)) {
            playerCtrl.CmdLaunchProjectile(1);
        }
        else if (Input.GetKeyDown(KeyCode.W)) {
            playerCtrl.CmdLaunchProjectile(4);
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            playerCtrl.CmdLaunchProjectile(5);
        }
        else if (Input.GetKeyDown(KeyCode.C)) {
            playerCtrl.CmdLaunchProjectile(6);
        }

        if (Input.touchCount > 0) {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                if (touch.position.x < (Screen.width/2)) {
                    //Left half
                    if(touch.position.y < (Screen.width/3))
                    {
                        playerCtrl.CmdLaunchProjectile(1);
                    }
                    else if(touch.position.y < (2*Screen.width/3))
                    {
                        playerCtrl.CmdLaunchProjectile(2);
                    }
                    else
                    {
                        playerCtrl.CmdLaunchProjectile(3);
                    }
                } else
                {
                    //Right half
                    if (touch.position.y < (Screen.width / 3))
                    {
                        playerCtrl.CmdLaunchProjectile(6);
                    }
                    else if (touch.position.y < (2 * Screen.width / 3))
                    {
                        playerCtrl.CmdLaunchProjectile(5);
                    }
                    else
                    {
                        playerCtrl.CmdLaunchProjectile(4);
                    }
                }
            }
        }
	}
}
