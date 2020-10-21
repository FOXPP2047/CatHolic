using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Kitties : MonoBehaviour
{
    private User currUser;
    IEnumerator GetUserData() {
        if (!string.IsNullOrEmpty(WebServices.CookieString)) {
            UnityWebRequest www = WebServices.Authenticated_Get("me");

            yield return www.SendWebRequest();

            if (www.isHttpError || www.isNetworkError) {
                Debug.Log(www.error);
            } else {
                Debug.Log(www.downloadHandler.text);
                currUser.username = JsonUtility.FromJson<User>(www.downloadHandler.text).username;
                currUser.scores = JsonUtility.FromJson<User>(www.downloadHandler.text).scores;
                
            }
        }
    }
}
