using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuSoundButton : MonoBehaviour
{
    private Button soundBtn;
    [HideInInspector]
    public bool isClicked = false;
    // Start is called before the first frame update
    void Start()
    {
        soundBtn = this.GetComponent<Button>();
        soundBtn.GetComponent<Image>().color = new Color(1, 1, 0);
        isClicked = false;
    }

    public void ClickBtn()
    {
        if(isClicked)
        {
            isClicked = false;
            soundBtn.GetComponent<Image>().color = new Color(1, 1, 0);
            
        } 
        else
        {
            isClicked = true;
            soundBtn.GetComponent<Image>().color = new Color(1, 1, 1);
        }
    }
}
