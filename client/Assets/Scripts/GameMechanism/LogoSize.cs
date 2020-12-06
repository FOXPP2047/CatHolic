using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoSize : MonoBehaviour
{
    private Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = this.GetComponent<Image>();

        Vector2 rectMiddle = new Vector2(0.5f, 0.5f);
        float horizontalSize = 1.0f;
        float verticalSize = 1.0f;
        img.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        img.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        img.GetComponent<RectTransform>().sizeDelta = new Vector2((float)(Screen.width), (float)(Screen.width / 4));
    }

    // Update is called once per frame
    void Update()
    {
        img.GetComponent<RectTransform>().sizeDelta = new Vector2((float)(Screen.width), (float)(Screen.width / 4));
    }
}
