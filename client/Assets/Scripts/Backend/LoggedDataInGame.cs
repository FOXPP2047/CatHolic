using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoggedDataInGame : MonoBehaviour
{
    public User currUser = new User();
    public Text currUsername;
    public Text currUserscores;
    public GameObject Cat1, Cat2, Cat3;
    public int countCat = 0;

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

        if (www.isHttpError || www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            currUser.username = JsonUtility.FromJson<User>(www.downloadHandler.text).username;
            currUser.scores = JsonUtility.FromJson<User>(www.downloadHandler.text).scores;
            currUser.items = JsonUtility.FromJson<User>(www.downloadHandler.text).items;
            currUsername.text = currUser.username;
            currUserscores.text = currUser.scores.ToString();

            if(countCat < currUser.items.Length)
            {
                itemSearch(countCat, currUser.items);
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

    public void itemSearch(int start, string[] items) {
        for (int i = start; i < items.Length; ++i) {
            Vector3 randomPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range((float)Screen.width * 0.1f, (float)Screen.width * 0.9f), Random.Range((float)Screen.height / 2, (float)Screen.height * 0.7f), Camera.main.farClipPlane / 2));
            if (items[i].Equals("Cat1"))
            {
                Instantiate(Cat1, randomPos, Cat1.transform.rotation);
                ++countCat;
            }
            else if (items[i].Equals("Cat2"))
            {
                Instantiate(Cat2, randomPos, Cat2.transform.rotation);
                ++countCat;
            }
            else if (items[i].Equals("Cat3"))
            {
                Instantiate(Cat3, randomPos, Cat3.transform.rotation);
                ++countCat;
            }
        }
    }
}
