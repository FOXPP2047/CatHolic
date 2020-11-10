using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    private SwipeScreen ss;
    private OpenStore os;
    private LoggedDataInGame logged;
    private bool ismoving;
    private RaycastHit? tempRaycastHit = null;
    private void Start()
    {
        ss = this.GetComponent<SwipeScreen>();
        os = this.GetComponent<OpenStore>();
        logged = this.GetComponent<LoggedDataInGame>();
        ismoving = false;
    }
    void Update()
    {
        if(!os.ItemStoreFrame.IsActive() && !logged.timeManagerBox.IsActive())
            ObjectMovement();

        if (!ismoving)
            ss.Swipe();
    }

    void ObjectMovement()
    {
        if(Input.GetMouseButtonDown(0) || ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began)))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 9)
                {
                    tempRaycastHit = hit;
                }
                else tempRaycastHit = null;
            }
        }

        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            if(tempRaycastHit.HasValue)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                tempRaycastHit.Value.rigidbody.useGravity = false;
                Vector3 pos = ray.origin + ray.direction * tempRaycastHit.Value.distance;
                pos.y -= 0.5f;
                pos.z = 100f;
                tempRaycastHit.Value.transform.position = pos;
                ismoving = true;
            }
        }
        else if (Input.GetMouseButtonUp(0) || Input.touchCount <= 0)
        {
            if (tempRaycastHit.HasValue)
            {
                tempRaycastHit.Value.rigidbody.useGravity = true;
                tempRaycastHit = null;
            }
            ismoving = false;
        }
    }
}
