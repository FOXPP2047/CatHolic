using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoClicker : MonoBehaviour
{
    private LoggedDataInGame logged;
    private float timer = 5.0f;
    private List<float> times;
    // Start is called before the first frame update
    void Start()
    {
        logged = this.GetComponent<LoggedDataInGame>();
        times = new List<float>();

        for(int i = 0; i < logged.currUser.autoCount.Length; ++i)
        {
            times.Add(0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(times.Count != logged.currUser.autoCount.Length)
        {
            for(int i = 0; i < logged.currUser.autoCount.Length - times.Count; ++i)
            {
                times.Add(0.0f);
            }
        }
        else
        {
            for (int i = 0; i < logged.currUser.autoCount.Length; ++i)
            {
                if(logged.currUser.autoCount[i] > 0)
                {
                    if (times[i] < logged.currUser.autoTime[i])
                    {
                        times[i] += Time.deltaTime;
                    }
                    else
                    {
                        logged.userScores += logged.currUser.autoCount[i];
                        times[i] = 0.0f;
                    }
                }
            }
        }
    }
}
