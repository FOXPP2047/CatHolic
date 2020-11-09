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
        string currTime = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        Debug.Log(currTime);
        WWWForm form = new WWWForm();
        form.AddField("username", currUser.username);
        form.AddField("time", currTime);
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
            currUser.locations = JsonUtility.FromJson<User>(www.downloadHandler.text).locations;
            currUser.recentLogin = JsonUtility.FromJson<User>(www.downloadHandler.text).recentLogin;
            currUser.recentLogout = JsonUtility.FromJson<User>(www.downloadHandler.text).recentLogout;

            currUsername.text = currUser.username;
            currUserscores.text = currUser.scores.ToString();

            if(!string.IsNullOrEmpty(currUser.recentLogin) && !string.IsNullOrEmpty(currUser.recentLogout))
            {
                System.DateTime login = System.DateTime.Parse(currUser.recentLogin);
                System.DateTime logout = System.DateTime.Parse(currUser.recentLogout);
                Debug.Log(login.ToString());
                Debug.Log(logout.ToString());
            }
            if(countCat < currUser.items.Length)
            {
                itemSearchLogin(currUser.items, currUser.locations);
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

    public void itemSearch(int start, string[] items, int[] locations) {
        for (int i = start; i < items.Length; ++i) {
            //Vector3 randomPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range((float)Screen.width * 0.1f, (float)Screen.width * 0.9f), Random.Range((float)Screen.height / 2, (float)Screen.height * 0.7f), Camera.main.farClipPlane / 2));
            Vector3 pos = this.GetComponent<SwipeScreen>().grounds[locations[i]].transform.position;
            float range = (this.GetComponent<SwipeScreen>().grounds[locations[i]].transform.localScale.x / 2) * 0.8f;
            float min = pos.x - range;
            float max = pos.x + range;

            Vector3 randomPos = new Vector3(Random.Range(min, max), pos.y + this.GetComponent<SwipeScreen>().grounds[locations[i]].transform.localScale.y / 2, pos.z);
            //Quaternion randomRotation = new Quaternion(Cat1.transform.rotation.x, Cat1.transform.rotation.y, Cat1.transform.rotation.z, );
            Quaternion randomRotation = Quaternion.AngleAxis((float)Random.Range(120, 180), new Vector3(0, 1, 0));
            if (items[i].Equals("Cat1"))
            {
                Instantiate(Cat1, randomPos, randomRotation);
                ++countCat;
            }
            else if (items[i].Equals("Cat2"))
            {
                Instantiate(Cat2, randomPos, randomRotation);
                ++countCat;
            }
            else if (items[i].Equals("Cat3"))
            {
                Instantiate(Cat3, randomPos, randomRotation);
                ++countCat;
            }
        }
    }

    public void itemSearchLogin(string[] items, int[] locations)
    {
        
        for (int i = 0; i < items.Length; ++i)
        {
            Vector3 pos = this.GetComponent<SwipeScreen>().grounds[locations[i]].transform.position;
            float range = (this.GetComponent<SwipeScreen>().grounds[locations[i]].transform.localScale.x / 2) * 0.5f;
            float min = pos.x - range;
            float max = pos.x + range;

            Vector3 randomPos = new Vector3(Random.Range(min, max), pos.y + this.GetComponent<SwipeScreen>().grounds[locations[i]].transform.localScale.y / 2, pos.z);
            //Quaternion randomRotation = new Quaternion(Cat1.transform.rotation.x, Cat1.transform.rotation.y, Cat1.transform.rotation.z, );
            Quaternion randomRotation = Quaternion.AngleAxis((float)Random.Range(120, 180), new Vector3(0, 1, 0));
            if (items[i].Equals("Cat1"))
            {
                Instantiate(Cat1, randomPos, randomRotation);
                ++countCat;
            }
            else if (items[i].Equals("Cat2"))
            {
                Instantiate(Cat2, randomPos, randomRotation);
                ++countCat;
            }
            else if (items[i].Equals("Cat3"))
            {
                Instantiate(Cat3, randomPos, randomRotation);
                ++countCat;
            }
        }
    }

    private void OnApplicationQuit()
    {
        StartCoroutine(GetLogOut());
    }
}
