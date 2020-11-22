using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class OpenStore : MonoBehaviour
{
    public Image ItemStoreFrame;
    public Image UpdateStoreFrame;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public Camera camera;
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

        UpdateStoreFrame.color = new Color32(0, 0, 0, 180);

        UpdateStoreFrame.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        UpdateStoreFrame.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        UpdateStoreFrame.GetComponent<RectTransform>().anchorMin = new Vector2(rectMiddle.x - horizontalSize / 2, rectMiddle.y - verticalSize / 2);
        UpdateStoreFrame.GetComponent<RectTransform>().anchorMax = new Vector2(rectMiddle.x + horizontalSize / 2, rectMiddle.y + verticalSize / 2);
    }

    public void OpenStoreButton() {
        if(!ItemStoreFrame.IsActive()) {
            ItemStoreFrame.gameObject.SetActive(true);
        }
    }

    public void CloseStoreButton() {
        if (Input.GetMouseButton(0)) {
            RectTransform rectTransform = ItemStoreFrame.GetComponent<RectTransform>();
            
            if (!RectTransformUtility.RectangleContainsScreenPoint(rectTransform, (Input.mousePosition), camera))
            {
                ItemStoreFrame.gameObject.SetActive(false);
                UpdateStoreFrame.gameObject.SetActive(false);
            } 
        }

        if(Input.touches.Length > 0) {
            RectTransform rectTransform = ItemStoreFrame.GetComponent<RectTransform>();

            if (!RectTransformUtility.RectangleContainsScreenPoint(rectTransform, (Input.GetTouch(0).position), camera))
            {
                ItemStoreFrame.gameObject.SetActive(false);
                UpdateStoreFrame.gameObject.SetActive(false);
            }
        }
    }

    public void SwipeStore()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                float size = Mathf.Abs(currentSwipe.x);
                //normalize the 2d vector
                currentSwipe.Normalize();


                //swipe left
                if (size > 150.0f && currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    if(ItemStoreFrame.IsActive())
                    {
                        ItemStoreFrame.gameObject.SetActive(false);
                        UpdateStoreFrame.gameObject.SetActive(true);
                    }
                }
                //swipe right
                if (size > 150.0f && currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    if (UpdateStoreFrame.IsActive())
                    {
                        UpdateStoreFrame.gameObject.SetActive(false);
                        ItemStoreFrame.gameObject.SetActive(true);
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            float size = Mathf.Abs(currentSwipe.x);
            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe left
            if (size > 150.0f && currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                if (ItemStoreFrame.IsActive())
                {
                    ItemStoreFrame.gameObject.SetActive(false);
                    UpdateStoreFrame.gameObject.SetActive(true);
                }
            }
            //swipe right
            if (size > 150.0f && currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                if (UpdateStoreFrame.IsActive())
                {
                    UpdateStoreFrame.gameObject.SetActive(false);
                    ItemStoreFrame.gameObject.SetActive(true);
                }
            }
        }
    }

    //void Update()
    //{
    //    if (ItemStoreFrame.IsActive() || UpdateStoreFrame.IsActive()) {
    //        SwipeStore();
    //        CloseStoreButton();
    //    }
    //}
}
