using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRangeColliderScript : MonoBehaviour
{
    public bool playerHasBeenDetectedInMovementRange;

    public GameObject gameObjectOfPlayerWhenHeGetsDetected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHasBeenDetectedInMovementRange = true;
            gameObjectOfPlayerWhenHeGetsDetected = collision.gameObject;
        }

    }

    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHasBeenDetectedInMovementRange = false;
            gameObjectOfPlayerWhenHeGetsDetected = null;
        }
    }
}
