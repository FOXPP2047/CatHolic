using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TappingButton : MonoBehaviour
{
    private User currUser = new User();
    public void GetLoggedData() {
        StartCoroutine(GetUserData());
    }
    IEnumerator SubmitScoreForm() {

        UnityWebRequest www = UnityWebRequest.Post(WebServices.mainUrl + "scores", "true");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            Debug.Log("Your Score will records right way.");
        }
        GetLoggedData();
    }

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
        } else {
            Debug.Log("CookieString is null or empty");
        }
    }
}
