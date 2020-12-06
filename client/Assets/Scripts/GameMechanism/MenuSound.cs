using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{
    private AudioSource bgm;
    public GameObject soundBtn;
    private MenuSoundButton msb;
    // Start is called before the first frame update
    void Start()
    {
        bgm = this.GetComponent<AudioSource>();
        msb = soundBtn.GetComponent<MenuSoundButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!msb.isClicked)
        {
            bgm.volume = 0.5f;
        }
        else
        {
            bgm.volume = 0.0f;
        }
    }
}
