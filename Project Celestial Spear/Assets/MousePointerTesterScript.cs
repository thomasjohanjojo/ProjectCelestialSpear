using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointerTesterScript : MonoBehaviour
{

    public Camera maincamera;

    private Vector3 mousePositionRaw;
    private Vector3 mousePositionConverted;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePositionRaw = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z);
        mousePositionConverted = maincamera.ScreenToWorldPoint(mousePositionRaw);
        transform.position = mousePositionConverted;
        
    }
}
