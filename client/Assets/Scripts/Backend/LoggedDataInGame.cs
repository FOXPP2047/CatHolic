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
    public Text currUserscores;
    public GameObject cat;

    private void Start() {
        StartCoroutine(GetUserData());
    }

    public void LogOut() {
        StartCoroutine(GetLogOut());
        SceneManager.LoadScene("SignIn");
    }

    public void GetScore() {
        StartCoroutine(SubmitScoreForm());
        StartCoroutine(GetUserData());
    }
    IEnumerator GetLogOut() {
        Debug.Log(currUser.username);
        WWWForm form = new WWWForm();
        form.AddField("username", currUser.username);
        UnityWebRequest www = UnityWebRequest.Post(WebServices.mainUrl + "logout", form);

        yield return www.SendWebRequest();
        if (www.isHttpError || www.isNetworkError) {
            Debug.Log(www.error);
        } else {
            Debug.Log(www.downloadHandler.text);            
        }
    }
    IEnumerator GetUserData() {
        //if (string.IsNullOrEmpty(WebServices.CookieString)) {
            UnityWebRequest www = WebServices.Authenticated_Get("me");

            yield return www.SendWebRequest();

            if (www.isHttpError || www.isNetworkError) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log(www.downloadHandler.text);
                currUser.username = JsonUtility.FromJson<User>(www.downloadHandler.text).username;
                currUser.scores = JsonUtility.FromJson<User>(www.downloadHandler.text).scores;
                currUser.items = JsonUtility.FromJson<User>(www.downloadHandler.text).items;
                currUsername.text = currUser.username;
                currUserscores.text = currUser.scores.ToString();

                foreach (string x in currUser.items)
                {
                    if (x.Equals("cat1"))
                    {
                        Instantiate(cat, new Vector3(0, 0, 0), Quaternion.identity);
                    }
                }
            }
        //}
    }

    IEnumerator SubmitScoreForm() {
        UnityWebRequest www = UnityWebRequest.Post(WebServices.mainUrl + "scores", "true");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        } else {
            Debug.Log("Your Score will records right way.");
        }
        StartCoroutine(GetUserData());
    }
}
