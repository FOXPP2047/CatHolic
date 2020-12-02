using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    enum eFadeType
    {
        fadeIn,
        fadeOut
    }

    private float alpha = 1.0f;
    private bool isFading = false;

    [SerializeField]
    private eFadeType fadeType = eFadeType.fadeOut;

    [SerializeField]
    private float fadingDuration = 100.0f;
    private float fadeSpeed = 1.0f;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if(fadeType == eFadeType.fadeIn)
        {
            alpha = 0.0f;
        }
        else if (fadeType == eFadeType.fadeOut)
        {
            alpha = 1.0f;
        }

        canvasGroup = this.GetComponent<CanvasGroup>();

        if(!canvasGroup)
        {
            Debug.LogError("[warning] CanvasFadeScript: please add a Canvas Group to the Canvas");
        }

        if(fadingDuration > 0)
        {
            fadeSpeed = 1 / fadingDuration;
        }

        StartFading();
    }

    public void StartFading()
    {
        setCanvasAlpha();
        isFading = true;
    }

    void setCanvasAlpha()
    {
        if(canvasGroup)
        {
            canvasGroup.alpha = alpha;
        }
    }

    private void Update()
    {
        if (isFading)
        {
            Debug.Log("Inside isFading Update");
            if (fadeType == eFadeType.fadeIn)
            {
                alpha += Time.deltaTime * fadeSpeed;
                if (alpha > 0.95f)
                {
                    CompletedFading();
                }
            }
            else if (fadeType == eFadeType.fadeOut)
            {
                alpha -= Time.deltaTime * fadeSpeed;
                if (alpha < 0.05f)
                {
                    CompletedFading();
                }
            }
            setCanvasAlpha();
        }
        else
        {
            Debug.Log("isFading is false");
        }
    }
    public void CompletedFading()
    {
        Debug.Log("Completed Fading");
        isFading = false;
        SceneManager.LoadScene("SignIn");
    }
}
