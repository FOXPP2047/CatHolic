using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoClicker : MonoBehaviour
{
    private LoggedDataInGame logged;
    private float timer = 5.0f;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        logged = this.GetComponent<LoggedDataInGame>();
    }

    // Update is called once per frame
    void Update()
    {
        if(time < timer)
        {
            time += Time.deltaTime;
        }
        else
        {
            logged.userScores += 10;
            time = 0.0f;
        }
    }
}
