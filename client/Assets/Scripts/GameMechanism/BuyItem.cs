﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class BuyItem : MonoBehaviour
{
    public Image currImg;
    public GameObject logged;
    private LoggedDataInGame data;
    private SwipeScreen screen;
    private void Start()
    {
        data = logged.GetComponent<LoggedDataInGame>();
        screen = logged.GetComponent<SwipeScreen>();
    }
    public void CatBringButton() {
        Debug.Log(currImg.gameObject.name);
        StartCoroutine(BuyItemForm(currImg.gameObject.name, screen.i));
    }

    IEnumerator BuyItemForm(string name, int location) {
        WWWForm form = new WWWForm();
        form.AddField("itemName", name);
        form.AddField("itemLocation", location);
        UnityWebRequest www = UnityWebRequest.Post(WebServices.mainUrl + "buy", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        } else {
            Debug.Log("Your Score will records right way.");
            StartCoroutine(GetUserData());
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
                data.currUser.username = JsonUtility.FromJson<User>(www.downloadHandler.text).username;
                data.currUser.scores = JsonUtility.FromJson<User>(www.downloadHandler.text).scores;
                data.currUser.items = JsonUtility.FromJson<User>(www.downloadHandler.text).items;
                data.currUser.locations = JsonUtility.FromJson<User>(www.downloadHandler.text).locations;
                data.currUsername.text = data.currUser.username;
                data.currUserscores.text = data.currUser.scores.ToString();
                data.itemSearch(data.countCat, data.currUser.items, data.currUser.locations);
            }
        //}
    }
}
