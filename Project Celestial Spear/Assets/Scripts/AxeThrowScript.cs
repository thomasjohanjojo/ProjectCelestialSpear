using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrowScript : MonoBehaviour
{

    public Transform transformOfThePlayer;

    public Vector3 directionToThePlayer;

    public Transform transformOfThisAxe;

    public Vector3 positionOfTheMouseClick;

    public float maxDistanceOfAxeTravel;

    private Vector3 directionToThePlayerWithMagnitude;

    public bool goBackToThePlayerAfterAttack = true;
    public bool goToTheEnemyToAttack = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceToPlayer();

        if(goToTheEnemyToAttack == true)
        {
            transform.Translate(positionOfTheMouseClick * Time.deltaTime);
            CheckIfAxeHasTravelledMaximumDistanceAndIfSoThenGoBackToPlayer();
            
        }

        else if(goBackToThePlayerAfterAttack == true)
        {
            GoBackToThePlayer();
        }

        DetectMousePositionWheneverFire2ButtonIsPressed();

    }

    public void DetectMousePositionWheneverFire2ButtonIsPressed()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            positionOfTheMouseClick = Input.mousePosition;
            positionOfTheMouseClick = Camera.main.ScreenToWorldPoint(positionOfTheMouseClick);
            positionOfTheMouseClick.z = transform.position.z;
            positionOfTheMouseClick.Normalize();
            goBackToThePlayerAfterAttack = false;
            goToTheEnemyToAttack = true;
            
        }
    }

    public void CheckDistanceToPlayer()
    {
        directionToThePlayer = transformOfThePlayer.position - transformOfThisAxe.position;
        directionToThePlayerWithMagnitude = directionToThePlayer;
    }

    public void GoBackToThePlayer()
    {
        
        directionToThePlayer.Normalize();
        transform.Translate(directionToThePlayer * Time.deltaTime);
    }

    public void CheckIfAxeHasTravelledMaximumDistanceAndIfSoThenGoBackToPlayer()
    {
        if(directionToThePlayerWithMagnitude.magnitude > maxDistanceOfAxeTravel)
        {
            goToTheEnemyToAttack = false;
            goBackToThePlayerAfterAttack = true;
        }
    }
    
}
