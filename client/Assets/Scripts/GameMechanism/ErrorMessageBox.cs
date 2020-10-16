using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ErrorMessageBox : MonoBehaviour
{
    public Image image;
    public Text text;
    public Button btn;

    public void ConfirmMessageBox()
    {
        image.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        text.GetComponent<Text>().color = new Color32(0, 0, 0, 0);
        text.text = "Error";
        image.gameObject.SetActive(false);
    }
}
