using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Events;

public class ConnectionManager : MonoBehaviour
{
    public InputField r_username;
    public InputField r_password;

    Coroutine registerationCoroutine;

    public UnityEvent formSubmitted = new UnityEvent();

    private void Start() {
        formSubmitted.AddListener(claerForm);
    }
    public void SubmitRegisterationForm() {
        if(registerationCoroutine == null) {
            registerationCoroutine = StartCoroutine(SubmitRegisterForm(r_username.text, r_password.text));
        }
        formSubmitted.Invoke();
    }

    void claerForm() {
        r_username.text = "";
        r_password.text = "";
    }
    IEnumerator SubmitRegisterForm(string username, string password) {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post(WebServices.mainUrl + "register", form);

        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        } else {
            Debug.Log("Form submitted correctly " + www.downloadHandler.text);
        }

        registerationCoroutine = null;
    }
}
