using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StoreButton : MonoBehaviour
{
    private User currUser = new User();
    public Text currUserscores;
    private void Start() {
        StartCoroutine(GetUserData());
    }
    public void BuyItem()  {
        StartCoroutine(BuyItemForm());
        StartCoroutine(GetUserData());
    }

    public void UpdateTappingButton() {
        StartCoroutine(UpdatingButtonScore());
        StartCoroutine(GetUserData());
    }
    IEnumerator BuyItemForm() {
        UnityWebRequest www = UnityWebRequest.Post(WebServices.mainUrl + "buy", "true");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            Debug.Log("Your Score will records right way.");
        }
    }
    IEnumerator UpdatingButtonScore()
    {
        UnityWebRequest www = UnityWebRequest.Post(WebServices.mainUrl + "updatebutton", "true");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) { 
            Debug.Log(www.error);
        }
        else {
            Debug.Log("Your Score will records right way.");
        }
    }
    IEnumerator GetUserData()
    {
        if (!string.IsNullOrEmpty(WebServices.CookieString)) {
            UnityWebRequest www = WebServices.Authenticated_Get("me");

            yield return www.SendWebRequest();

            if (www.isHttpError || www.isNetworkError) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log(www.downloadHandler.text);
                currUser.username = JsonUtility.FromJson<User>(www.downloadHandler.text).username;
                currUser.scores = JsonUtility.FromJson<User>(www.downloadHandler.text).scores;
                currUserscores.text = currUser.scores.ToString();
            }
        }
    }
}
