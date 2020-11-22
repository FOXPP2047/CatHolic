using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebServices : MonoBehaviour
{
    public static readonly string mainUrl = "http://52.13.57.174:8000/";//"http://localhost:8000/";//

    public static string CookieString {
        get {
            return PlayerPrefs.GetString("cookie");
        }
        set {
            PlayerPrefs.SetString("cookie", value);
        }
    }

    public static UnityWebRequest Authenticated_Get(string path) {
        UnityWebRequest www = new UnityWebRequest(mainUrl + path);

        if (!string.IsNullOrEmpty(CookieString)) {
            www.SetRequestHeader("cookie", CookieString);
        }

        www.downloadHandler = new DownloadHandlerBuffer();

        return www;
    }

    public static UnityWebRequest GetLogOut(string path) {
        UnityWebRequest www = new UnityWebRequest(mainUrl + path);

        www.downloadHandler = new DownloadHandlerBuffer();

        return www;
    }
}
