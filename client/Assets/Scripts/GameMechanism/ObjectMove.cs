using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class ObjectMove : MonoBehaviour
{
    private OpenStore os;
    private LoggedDataInGame logged;
    [HideInInspector]
    public bool ismoving;
    private RaycastHit? tempRaycastHit = null;
    [HideInInspector]
    public bool isClicked;

    public Text selectedCat;
    public GameObject btns;
    private int i = 0;
    private string tempname;
    private void Start()
    {
        os = this.GetComponent<OpenStore>();
        logged = this.GetComponent<LoggedDataInGame>();
        ismoving = false;
        isClicked = false;
    }
    void Update()
    {
        if (!os.ItemStoreFrame.IsActive() && !logged.timeManagerBox.IsActive())
            ObjectMovement();

        if(btns.activeSelf)
        {
            selectedCat.text = tempname + " " + logged.currUser.autoCount[i].ToString() + " / " + logged.currUser.autoTime[i].ToString();
        }
    }

    public void UpdateAutoCount()
    {
        if(logged.userScores >= 10)
        {
            logged.userScores -= 10;
            logged.currUser.autoCount[i] += 1;
            StartCoroutine(SendAutoDataCount(i, logged.currUser.autoCount[i]));
        }
        
        //Debug.Log(logged.currUser.autoCount[i]);
    }

    public void UpdateAutoTime()
    {
        if(logged.userScores >= 10)
        {
            logged.userScores -= 10;
            logged.currUser.autoTime[i] -= 1;
            StartCoroutine(SendAutoDataTime(i, logged.currUser.autoTime[i]));
        }
        
        //Debug.Log(logged.currUser.autoTime[i]);
    }
    void ObjectMovement()
    {
        if(Input.GetMouseButtonDown(0) || ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began)))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 9)
                {
                    tempRaycastHit = hit;
                    tempname = tempRaycastHit.Value.transform.name;
                    i = int.Parse(tempname.Substring(tempname.Length - 1));
                    selectedCat.text = tempname + " " + logged.currUser.autoCount[i].ToString() + " / " + logged.currUser.autoTime[i].ToString();
                    btns.SetActive(true);
                }
                else
                {
                    tempRaycastHit = null;
                    selectedCat.text = "";
                    btns.SetActive(false);
                }
            }
        }

        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            if(tempRaycastHit.HasValue)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                tempRaycastHit.Value.rigidbody.useGravity = false;
                Vector3 pos = ray.origin + ray.direction * tempRaycastHit.Value.distance;
                pos.y -= 0.5f;
                pos.z = 100f;
                tempRaycastHit.Value.transform.position = pos;
                ismoving = true;
            }
        }
        else if (Input.GetMouseButtonUp(0) || Input.touchCount <= 0)
        {
            if (tempRaycastHit.HasValue)
            {
                tempRaycastHit.Value.rigidbody.useGravity = true;
                tempRaycastHit = null;
            }
            ismoving = false;
        }
    }

    IEnumerator SendAutoDataCount(int i, int count)
    {
        WWWForm form = new WWWForm();
        form.AddField("index", i);
        form.AddField("count", count);
        form.AddField("score", logged.userScores);
        UnityWebRequest www = UnityWebRequest.Post(WebServices.mainUrl + "autocount", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Your Score will records right way.");
        }
        //logged.GetPureDataOnly();
    }

    IEnumerator SendAutoDataTime(int i, int time)
    {
        WWWForm form = new WWWForm();
        form.AddField("index", i);
        form.AddField("time", time);
        form.AddField("score", logged.userScores);
        UnityWebRequest www = UnityWebRequest.Post(WebServices.mainUrl + "autotime", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Your Score will records right way.");
        }
        //logged.GetPureDataOnly();
    }
}
