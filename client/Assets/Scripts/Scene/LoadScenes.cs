using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public void LoadSignInScene() {
        SceneManager.LoadScene("SignIn");
    }

    public void LoadSignUpScene() {
        SceneManager.LoadScene("SignUp");
    }

    public void LoadCreditScene() {
        SceneManager.LoadScene("Credit");
    }
}
