using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toastr : MonoBehaviour {

    public GameObject toastPrefab;

    private GameObject toast;
	
	public void Toast(string message)
    {
        if(toast)
        {
            StartCoroutine(ProcrastinateToast(1, message));
            return;
        }
        toast = GameObject.Instantiate(toastPrefab, gameObject.transform.parent.transform);
        toast.GetComponent<Image>().CrossFadeAlpha(0f, 0, true);
        toast.GetComponentInChildren<Text>().CrossFadeAlpha(0f, 0, true);
        toast.GetComponent<Image>().CrossFadeAlpha(0.9f, 0.5f, false);
        toast.GetComponentInChildren<Text>().CrossFadeAlpha(0.9f, 0.5f, false);

        toast.GetComponentInChildren<Text>().text = message;
        StartCoroutine(Clear());
    }

    IEnumerator ProcrastinateToast(int delaySeconds, string toastContent)
    {
        yield return new WaitForSeconds(delaySeconds);
        Toast(toastContent);
    }

    IEnumerator Clear()
    {
        yield return new WaitForSeconds(2);
        toast.GetComponent<Image>().CrossFadeAlpha(0f, 0.5f, false);
        toast.GetComponentInChildren<Text>().CrossFadeAlpha(0f, 0.5f, false);
        GameObject.Destroy(toast,0.5f);
        toast = null;
    }
}
