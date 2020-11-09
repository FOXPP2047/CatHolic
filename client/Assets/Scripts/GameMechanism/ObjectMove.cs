using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    private SwipeScreen ss;
    private bool ismoving;
    private void Start()
    {
        ss = this.GetComponent<SwipeScreen>();
        ismoving = false;
    }
    void Update()
    {
        ObjectMovement();

        if (!ismoving) ss.Swipe();
    }

    void ObjectMovement()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Cat Layer
                if (hit.transform.gameObject.layer == 9)
                {
                    //Vector3 localHit = hit.transform.position;
                    //hit.transform.position = new Vector3(hit.point.x, hit.point.y - 0.5f, hit.point.z);
                    Vector3 pos = hit.point;
                    pos.y -= 0.5f;
                    pos.z = 100;
                    hit.transform.position = pos;
                    ismoving = true;
                }
                else ismoving = false;
            }
            else ismoving = false;
        }
        else if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Cat Layer
                if (hit.transform.gameObject.layer == 9)
                {
                    Vector3 pos = hit.point;
                    pos.y -= 0.5f;
                    pos.z = 100;
                    hit.transform.position = pos;
                    ismoving = true;
                }
                else ismoving = false;
            }
            else ismoving = false;
        }
    }
}
