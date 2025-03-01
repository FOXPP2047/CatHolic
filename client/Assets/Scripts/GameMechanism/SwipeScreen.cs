﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeScreen : MonoBehaviour
{
    private OpenStore os;
    private Transform cameraTransform;
    private ObjectMove om;
    public GameObject[] grounds;
    [HideInInspector]
    public int i = 0;
    
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    // Start is called before the first frame update
    void Start()
    {
        os = this.GetComponent<OpenStore>();
        om = this.GetComponent<ObjectMove>();
        cameraTransform = Camera.main.transform;
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!os.ItemStoreFrame.IsActive() && !os.UpdateStoreFrame.IsActive())
            Swipe();
        else if(os.ItemStoreFrame.IsActive() || os.UpdateStoreFrame.IsActive())
        {
            os.SwipeStore();
            os.CloseStoreButton();
        }
    }

    public void Swipe()
    {
        if(!om.ismoving)
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

                    //swipe upwards
                    //if (currentSwipe.y > 0  currentSwipe.x > -0.5f  currentSwipe.x < 0.5f)
                    //{
                    //    Debug.Log("up swipe");
                    //}

                    //swipe down
                    //if (currentSwipe.y < 0  currentSwipe.x > -0.5f  currentSwipe.x < 0.5f) 
                    //{
                    //    Debug.Log("down swipe");
                    //}

                    //swipe left
                    if (size > 150.0f && currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        if (i < grounds.Length - 1)
                        {
                            ++i;
                            Vector3 endPos = new Vector3(grounds[i].transform.position.x, cameraTransform.position.y, cameraTransform.position.z);
                            Camera.main.transform.position = endPos;// Vector3.Lerp(Camera.main.transform.position, endPos, Time.deltaTime * 100);
                                                                    //Camera.main.transform.Translate(grounds[i].transform.position.x, cameraTransform.position.y, cameraTransform.position.z);
                        }
                    }
                    //swipe right
                    if (size > 150.0f && currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        if (i > 0)
                        {
                            --i;
                            Vector3 endPos = new Vector3(grounds[i].transform.position.x, cameraTransform.position.y, cameraTransform.position.z);
                            Camera.main.transform.position = endPos;// Vector3.Lerp(Camera.main.transform.position, endPos, Time.deltaTime * 100);
                                                                    //Camera.main.transform.Translate(grounds[i].transform.position.x, cameraTransform.position.y, cameraTransform.position.z);
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

                ////swipe upwards
                //if (currentSwipe.y > 0  currentSwipe.x > -0.5f  currentSwipe.x < 0.5f)
                //{
                //    Debug.Log("up swipe");
                //}
                ////swipe down
                //if (currentSwipe.y < 0  currentSwipe.x > -0.5f  currentSwipe.x < 0.5f)
                //{
                //    Debug.Log("down swipe");
                //}
                //swipe left
                if (size > 150.0f && currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    if (i < grounds.Length - 1)
                    {
                        ++i;
                        Vector3 endPos = new Vector3(grounds[i].transform.position.x, cameraTransform.position.y, cameraTransform.position.z);
                        Camera.main.transform.position = endPos;// Vector3.Lerp(Camera.main.transform.position, endPos, Time.deltaTime * 100);
                                                                //Camera.main.transform.Translate(grounds[i].transform.position.x, cameraTransform.position.y, cameraTransform.position.z);
                    }
                }
                //swipe right
                if (size > 150.0f && currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    if (i > 0)
                    {
                        --i;
                        Vector3 endPos = new Vector3(grounds[i].transform.position.x, cameraTransform.position.y, cameraTransform.position.z);
                        Camera.main.transform.position = endPos;// Vector3.Lerp(Camera.main.transform.position, endPos, Time.deltaTime * 100);
                                                                //Camera.main.transform.Translate(grounds[i].transform.position.x, cameraTransform.position.y, cameraTransform.position.z);
                    }
                }
            }
        }
    }
}
