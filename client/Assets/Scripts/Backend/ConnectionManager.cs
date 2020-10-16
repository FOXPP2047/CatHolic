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

    public Image errorMessageBox;
    public Text errorMessage;

    Coroutine registerationCoroutine;

    public UnityEvent formSubmitted = new UnityEvent();

    private void Start() {
        formSubmitted.AddListener(claerForm);
        errorMessageBox.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        errorMessage.GetComponent<Text>().color = new Color32(0, 0, 0, 0);
        errorMessage.gameObject.SetActive(false);
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
            errorMessageBox.gameObject.SetActive(true);
            errorMessage.gameObject.SetActive(true);
            errorMessage.text = www.error;
            errorMessageBox.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            errorMessage.GetComponent<Text>().color = new Color32(0, 0, 0, 255);
        } else {
            Debug.Log("Form submitted correctly " + www.downloadHandler.text);
        }

        registerationCoroutine = null;
    }
}
