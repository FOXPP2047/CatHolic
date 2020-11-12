using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    private OpenStore os;
    private LoggedDataInGame logged;
    public bool ismoving;
    private RaycastHit? tempRaycastHit = null;
    public bool isClicked;
    private void Start()
    {
        os = this.GetComponent<OpenStore>();
        logged = this.GetComponent<LoggedDataInGame>();
        ismoving = false;
        isClicked = false;
    }
    void Update()
    {
        if (!os.ItemStoreFrame.IsActive() && !logged.timeManagerBox.IsActive())
            ObjectMovement();
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
