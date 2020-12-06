using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadScenes : MonoBehaviour
{
    public Button MenuSoundBtn;
    private AudioSource submitAS;

    private void Start()
    {
        submitAS = this.GetComponent<AudioSource>();
    }
    public void LoadSignInScene() {
        if(MenuSoundBtn)
        {
            if (!MenuSoundBtn.GetComponent<MenuSoundButton>().isClicked)
                submitAS.Play();
        }
        
        SceneManager.LoadScene("SignIn");
    }

    public void LoadSignUpScene() {
        if (MenuSoundBtn)
        {
            if (!MenuSoundBtn.GetComponent<MenuSoundButton>().isClicked)
                submitAS.Play();
        }
        SceneManager.LoadScene("SignUp");
    }

    public void LoadCreditScene() {
        if (MenuSoundBtn)
        {
            if (!MenuSoundBtn.GetComponent<MenuSoundButton>().isClicked)
                submitAS.Play();
        }
        SceneManager.LoadScene("Credit");
    }
}
