using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSize : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        //float depth = this.transform.lossyScale.z;
        //float width = this.transform.lossyScale.x;
        //float height = this.transform.lossyScale.y;

        //Vector3 lowerLeftPoint = Camera.main.WorldToScreenPoint(new Vector3(this.transform.position.x - width / 2, this.transform.position.y - height / 2, this.transform.position.z - depth / 2));
        //Vector3 upperRightPoint = Camera.main.WorldToScreenPoint(new Vector3(this.transform.position.x + width / 2, this.transform.position.y + height / 2, this.transform.position.z - depth / 2));
        //Vector3 upperLeftPoint = Camera.main.WorldToScreenPoint(new Vector3(this.transform.position.x - width / 2, this.transform.position.y + height / 2, this.transform.position.z - depth / 2));
        //Vector3 lowerRightPoint = Camera.main.WorldToScreenPoint(new Vector3(this.transform.position.x + width / 2, this.transform.position.y - height / 2, this.transform.position.z - depth / 2));

        //float xPixelDistance = Mathf.Abs(lowerLeftPoint.x - upperRightPoint.x);
        //float yPixelDistance = Mathf.Abs(lowerLeftPoint.y - upperRightPoint.y);

        //print("This is the X pixel distance: " + xPixelDistance + " This is the Y pixel distance: " + yPixelDistance);
        //print("This is the lower left pixel point: " + lowerLeftPoint);
        //print(" This is the upper left point: " + upperLeftPoint);
        //print("This is the lower right pixel point: " + lowerRightPoint);
        //print(" This is the upper right pixel point: " + upperRightPoint);

        //this.transform.localScale = new Vector3(Screen.width / 171, (float)Screen.height / 7362, Screen.width / 171);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
