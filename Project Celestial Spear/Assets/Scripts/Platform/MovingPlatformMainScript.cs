using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformMainScript : MonoBehaviour
{

    

    
    public float movementSpeedOfPlatform;
    

    public Transform transformOfUpperWayPoint;
    public Transform transformOfLowerWayPoint;

    private Vector2 theVector2OfLowerWaypoint;
    private Vector2 theVector2OfUpperWaypoint;

    private bool moveDownwards;
    private bool moveUpwards;

    // Start is called before the first frame update
    void Start()
    {

        moveUpwards = true;

    }



    // Update is called once per frame
    void Update()
    {
        CheckAndSetTheTwoControlBooleansAccordingToTheYLimits();
        TheActualMovementFunctionAccordingToTheStatusOfTheControlBooleans();
    }

    private void TheActualMovementFunctionAccordingToTheStatusOfTheControlBooleans()
    {
        if (moveUpwards == true)
        {
            MoveUpwards();
        }

        else if (moveDownwards == true)
        {
            MoveDownwards();
        }
    }

    private void CheckAndSetTheTwoControlBooleansAccordingToTheYLimits()
    {
        if (transform.position.y > transformOfUpperWayPoint.position.y)
        {
            moveUpwards = false;
            moveDownwards = true;
        }

        else if (transform.position.y < transformOfLowerWayPoint.position.y)
        {
            moveDownwards = false;
            moveUpwards = true;
        }
    }

    private void MoveUpwards()
    {
        transform.Translate(Vector3.up * Time.deltaTime * movementSpeedOfPlatform);
    }

    private void MoveDownwards()
    {
        transform.Translate(Vector3.down * Time.deltaTime * movementSpeedOfPlatform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.SetParent(null);
    }

    
}
