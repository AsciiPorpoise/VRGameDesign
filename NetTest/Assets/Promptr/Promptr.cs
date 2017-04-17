using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Promptr : MonoBehaviour {

    public GameObject promptPrefab;

    private GameObject prompt;

    public delegate void PromptCallback(string input);

    public void Prompt(string message, PromptCallback callback)
    {
        if(prompt)
        {
            StartCoroutine(ProcrastinatePrompt(1, message, callback));
            return;
        }
        prompt = GameObject.Instantiate(promptPrefab, gameObject.transform.parent.transform);
        prompt.GetComponent<Image>().CrossFadeAlpha(0f, 0, true);
        prompt.GetComponentInChildren<Text>().CrossFadeAlpha(0f, 0, true);
        prompt.GetComponent<Image>().CrossFadeAlpha(2f, 0.5f, false);
        prompt.GetComponentInChildren<Text>().CrossFadeAlpha(2f, 0.5f, false);

        prompt.GetComponentInChildren<Text>().text = message;
        prompt.GetComponentInChildren<Button>().onClick.AddListener(() => Clear(callback));
    }

    IEnumerator ProcrastinatePrompt(int delaySeconds, string toastContent, PromptCallback callback)
    {
        yield return new WaitForSeconds(delaySeconds);
        Prompt(toastContent, callback);
    }

    void Clear(PromptCallback callback)
    {
        string input = prompt.GetComponentInChildren<InputField>().text;

        prompt.GetComponent<Image>().CrossFadeAlpha(0f, 0.5f, false);
        prompt.GetComponentInChildren<Text>().CrossFadeAlpha(2f, 0.5f, false);
        GameObject.Destroy(prompt,0.5f);
        prompt = null;

        callback(input);
    }
}
