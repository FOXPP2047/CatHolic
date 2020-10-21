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
    private bool isLoggedin = false;
    private User currUser = new User();

    public UnityEvent formSubmitted = new UnityEvent();

    private const string COOKIE_HEADER_KEY = "set-cookie";
    private const string COOKIE_AUTH_SESSION = "connect.sid";

    public Image errorMessageBox;
    public Text errorMessage;

    private void Start() {
        formSubmitted.AddListener(claerForm);
        //Debug.Log(currUser.username);
        //GetLoggedData();
    }

    private void Update() {
        if (isLoggedin) {
            SceneManager.LoadScene("InGame");
        }
        //Debug.Log(WebServices.CookieString);
    }
    public void SubmitLogInForm() {
        if (loginCoroutine == null) {
            loginCoroutine = StartCoroutine(SubmitLoginForm(l_username.text, l_password.text));
        }
        formSubmitted.Invoke();
    }

    public User getUser() {
        return currUser;
    }
    public void LogOut() {
        WebServices.CookieString = string.Empty;
        //currUser.username = null;
        //currUser.username = string.Empty;
        GetLoggedData();
        //SceneManager.LoadScene("SignIn");
    }

    public void GetLoggedData() {
        StartCoroutine(GetUserData());
    }

    public bool IsUserLoggedIn() {
        if (!string.IsNullOrEmpty(WebServices.CookieString))
            return true;
        else return false;
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

        string storedCookie = www.GetResponseHeader(COOKIE_HEADER_KEY);
        Debug.Log(storedCookie);
        WebServices.CookieString = storedCookie;
        Debug.Log(storedCookie);
        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
            errorMessageBox.gameObject.SetActive(true);
            errorMessage.gameObject.SetActive(true);
            errorMessage.text = www.error;
            errorMessageBox.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            errorMessage.GetComponent<Text>().color = new Color32(0, 0, 0, 255);
        } else { 
            Debug.Log("LogIn Form submitted correctly " + www.downloadHandler.text);
            isLoggedin = true;
        }
        GetLoggedData();
    }

    IEnumerator GetUserData() {
        if(!string.IsNullOrEmpty(WebServices.CookieString)) {
            //Debug.Log("Here in");
            UnityWebRequest www = WebServices.Authenticated_Get("me");

            yield return www.SendWebRequest();

            if (www.isHttpError || www.isNetworkError) {
                Debug.Log(www.error);
                errorMessageBox.gameObject.SetActive(true);
                errorMessage.gameObject.SetActive(true);
                errorMessage.text = www.error;
                errorMessageBox.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
                errorMessage.GetComponent<Text>().color = new Color32(0, 0, 0, 255);
            } else { 
                Debug.Log(www.downloadHandler.text);
                currUser.username = JsonUtility.FromJson<User>(www.downloadHandler.text).username;
                currUser.scores = JsonUtility.FromJson<User>(www.downloadHandler.text).scores;
            }
        } else {
            Debug.Log("CookieString is null or empty");
        }
    }

    void BringCats() {

    }
}

[System.Serializable]
public class User {
    public string username;
    public int scores;
    public string[] items;
}
