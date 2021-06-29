using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelSectionsControllerScript : MonoBehaviour
{


    public bool thisSectionHasThePlayerInIt;

    public CinemachineVirtualCamera virtualCameraOfCinemachine;

    // Start is called before the first frame update
    void Start()
    {
        thisSectionHasThePlayerInIt = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(thisSectionHasThePlayerInIt == true)
        {
            virtualCameraOfCinemachine.LookAt = gameObject.transform;
            virtualCameraOfCinemachine.Follow = gameObject.transform;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            thisSectionHasThePlayerInIt = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            thisSectionHasThePlayerInIt = false;
        }
    }

}
