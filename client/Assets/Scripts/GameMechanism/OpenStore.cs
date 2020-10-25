﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class OpenStore : MonoBehaviour
{
    public Image ItemStoreFrame;
    void Start()
    {
        ItemStoreFrame.color = new Color32(0, 0, 0, 180);
        
        Vector2 rectMiddle = new Vector2(0.5f, 0.5f);
        float horizontalSize = 0.9f;
        float verticalSize = 0.9f;
        ItemStoreFrame.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        ItemStoreFrame.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        ItemStoreFrame.GetComponent<RectTransform>().anchorMin = new Vector2(rectMiddle.x - horizontalSize / 2, rectMiddle.y - verticalSize / 2);
        ItemStoreFrame.GetComponent<RectTransform>().anchorMax = new Vector2(rectMiddle.x + horizontalSize / 2, rectMiddle.y + verticalSize / 2);
    }

    public void OpenStoreButton() {
        if(!ItemStoreFrame.IsActive()) {
            ItemStoreFrame.gameObject.SetActive(true);
        }
    }

    public void CloseStoreButton() {
        Camera camera = null;

        if(Input.GetMouseButton(0)) {
            RectTransform rectTransform = ItemStoreFrame.GetComponent<RectTransform>();

            if (!RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition, camera))
                ItemStoreFrame.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (ItemStoreFrame.IsActive()) {
            CloseStoreButton();
            //CatBringButton();
            //GetObjectName();
        }
    }
}
