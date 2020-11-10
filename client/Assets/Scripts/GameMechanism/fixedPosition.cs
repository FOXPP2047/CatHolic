using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixedPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.y < -0.5f)
        {
            Vector3 newPos = new Vector3(this.gameObject.transform.position.x, 5, this.gameObject.transform.position.z);
            this.gameObject.transform.position = newPos;
        }
            
    }
}
