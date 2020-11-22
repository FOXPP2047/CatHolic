using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TimeBoxManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image;
    public Text text;
    public Button btn;
    public GameObject connectionObj;
    private LoggedDataInGame logged;
    private void Start()
    {
        logged = connectionObj.GetComponent<LoggedDataInGame>();
    }
    public void ConfirmMessageBox()
    {
        StartCoroutine(SubmitRecentLoginForm());
        logged.userScores += logged.tempScores;
        logged.GetScoreUpdate();
        image.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        text.GetComponent<Text>().color = new Color32(0, 0, 0, 0);
        text.text = "Error";
        image.gameObject.SetActive(false);
    }

    IEnumerator SubmitRecentLoginForm() {
        WWWForm form = new WWWForm();
        form.AddField("recentTime", logged.totalTime);

        UnityWebRequest www = UnityWebRequest.Post(WebServices.mainUrl + "recentLogin", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        } else {
            Debug.Log("Your Score will records right way.");
        }
    }
}
