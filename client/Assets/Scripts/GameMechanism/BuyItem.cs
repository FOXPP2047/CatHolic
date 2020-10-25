using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class BuyItem : MonoBehaviour
{
    public Image currImg;
    public void CatBringButton() {
        StartCoroutine(BuyItemForm(currImg.gameObject.name));
    }

    IEnumerator BuyItemForm(string name) {
        WWWForm form = new WWWForm();
        form.AddField("itemName", name);
        UnityWebRequest www = UnityWebRequest.Post(WebServices.mainUrl + "buy", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        } else {
            Debug.Log("Your Score will records right way.");
        }
    }

    IEnumerator GetUserData() {
        //if (string.IsNullOrEmpty(WebServices.CookieString)) {
            UnityWebRequest www = WebServices.Authenticated_Get("me");

            yield return www.SendWebRequest();

            if (www.isHttpError || www.isNetworkError) {
                Debug.Log(www.error);
            } else {
                Debug.Log(www.downloadHandler.text);
                //currUser.username = JsonUtility.FromJson<User>(www.downloadHandler.text).username;
                //currUser.scores = JsonUtility.FromJson<User>(www.downloadHandler.text).scores;
                //currUser.items = JsonUtility.FromJson<User>(www.downloadHandler.text).items;
                //currUserscores.text = currUser.scores.ToString();
            }
        //}
    }
}
