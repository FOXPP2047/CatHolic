using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateValueText : MonoBehaviour
{
    public GameObject connectionObj;
    private Text updateText;
    // Start is called before the first frame update
    void Start()
    {
        updateText = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        updateText.text = "Update : " + connectionObj.GetComponent<LoggedDataInGame>().updateScores;
    }
}
