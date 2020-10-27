using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    void Update()
    {
        ObjectMovement();
    }

    void ObjectMovement() {

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Cat Layer
                if(hit.transform.gameObject.layer == 9)
                    hit.transform.position = new Vector3(hit.point.x, hit.point.y - 0.5f, hit.point.z);
            }
        }
        else if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Cat Layer
                if (hit.transform.gameObject.layer == 9)
                    hit.transform.position = hit.point;
            }
        }
    }
}
