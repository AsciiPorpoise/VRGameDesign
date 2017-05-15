using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour {

    Slider slider;

    void Start()
    {
        slider = this.GetComponent<Slider>();
    }

	void Set(float val)
    {
        slider.value = val / 100f;
    }

    public int Get()
    {
        return (int)(slider.value*100f);
    }

    public void Hit(float val)
    {
        Set(Get() - val);
    }

    public void ToastAndQuitIfDead()
    {
        if(slider.value <= 0f)
        {
            GameObject.Find("Toastr").GetComponent<Toastr>().Toast("GAME OVER");
            StartCoroutine(GameController.Reboot(3));
        }
    }
}
