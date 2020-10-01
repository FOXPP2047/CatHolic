using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LogInMannger : MonoBehaviour
{
    public InputField l_username;
    public InputField l_password;
    Coroutine loginCoroutine;

    private User currUser = new User();

    public UnityEvent formSubmitted = new UnityEvent();

    private const string COOKIE_HEADER_KEY = "set-cookie";
    private const string COOKIE_AUTH_SESSION = "connect.sid";

    private void Start() {
        formSubmitted.AddListener(claerForm);
        //GetLoggedData();
    }

    private void Update() {
        if (IsUserLoggedIn()) {
            SceneManager.LoadScene("InGame");
        }
    }
    public void SubmitLogInForm() {
        if (loginCoroutine == null) {
            loginCoroutine = StartCoroutine(SubmitLoginForm(l_username.text, l_password.text));
        }
        formSubmitted.Invoke();
    }

    public void LogOut() {
        WebServices.CookieString = string.Empty;
    }

    public void GetLoggedData() {
        StartCoroutine(GetUserData());
    }

    public bool IsUserLoggedIn() {
        if (string.IsNullOrEmpty(currUser.username))
            return false;
        else return true;
    }
    void claerForm() {
        l_username.text = "";
        l_password.text = "";
    }
    IEnumerator SubmitLoginForm(string username, string password) {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        
        UnityWebRequest www = UnityWebRequest.Post(WebServices.mainUrl + "login", form);

        yield return www.SendWebRequest();

        string storedCookie = www.GetRequestHeader(COOKIE_HEADER_KEY);
        WebServices.CookieString = storedCookie;

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        } else { 
            Debug.Log("LogIn Form submitted correctly " + www.downloadHandler.text);
            StartCoroutine(GetUserData());
        }
    }

    IEnumerator GetUserData() {
        if(string.IsNullOrEmpty(WebServices.CookieString)) {
            UnityWebRequest www = WebServices.Authenticated_Get("me");

            yield return www.SendWebRequest();

            if (www.isHttpError || www.isNetworkError) {
                Debug.Log(www.error);
            }
            else { 
                Debug.Log(www.downloadHandler.text);
                currUser = JsonUtility.FromJson<User>(www.downloadHandler.text);
                //loggedInData.text = user.username;
            }
        }
    }
}

[System.Serializable]
public class User {
    public string username;
}
