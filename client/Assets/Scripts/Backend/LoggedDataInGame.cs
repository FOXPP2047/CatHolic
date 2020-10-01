using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoggedDataInGame : MonoBehaviour
{
    private User currUser = new User();
    public Text currUsername;

    private void Start() {
        StartCoroutine(GetUserData());
    }
    public void LogOut() {
        WebServices.CookieString = string.Empty;
        SceneManager.LoadScene("SignIn");
    }

    IEnumerator GetUserData() {
        if (string.IsNullOrEmpty(WebServices.CookieString)) {
            UnityWebRequest www = WebServices.Authenticated_Get("me");

            yield return www.SendWebRequest();

            if (www.isHttpError || www.isNetworkError) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log(www.downloadHandler.text);
                currUser = JsonUtility.FromJson<User>(www.downloadHandler.text);
                currUsername.text = currUser.username;
            }
        }
    }
}
