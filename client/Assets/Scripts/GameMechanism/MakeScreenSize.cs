using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeScreenSize : MonoBehaviour
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

        img.GetComponent<RectTransform>().anchorMin = new Vector2(rectMiddle.x - horizontalSize / 2, rectMiddle.y - verticalSize / 2);
        img.GetComponent<RectTransform>().anchorMax = new Vector2(rectMiddle.x + horizontalSize / 2, rectMiddle.y + verticalSize / 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
