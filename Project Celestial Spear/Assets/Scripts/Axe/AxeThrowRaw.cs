using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrowRaw : MonoBehaviour
{
    public Transform transformOfThePlayer;

    public Vector3 directionToThePlayer;

    public Transform transformOfThisAxe;

    public Vector3 positionOfTheMouseClick;
    

    public float maxDistanceOfAxeTravel;

    public float speedOfAxeThrow;
    public int damageOfTheAxeThrow;

    public BoxCollider2D colliderOfTheAxe;
     
    private Rigidbody2D enemyRigidBody;

    private Statuses statusScriptOfTheEnemy;

    public PlayerStateController playerStateControllerReference;

    private Vector3 directionToThePlayerWithMagnitude;

    public bool axeThrowScriptOnOffBoolean;

    public bool goBackToThePlayerAfterAttack = false;
    public bool goToTheEnemyToAttack = false;
    public bool pressingTheRangedAttackButtonIsPossibleAndCanThrowAxe = true;

    private bool StayWithThePlayerWhileAxeIsNotThrowing;

    public bool axeThrowHasBeenPressedOnceBeforeReturnJourney = false;



    // Start is called before the first frame update
    void Start()
    {
        StayWithThePlayerWhileAxeIsNotThrowing = true;
        axeThrowScriptOnOffBoolean = false;
        goBackToThePlayerAfterAttack = false;
        goToTheEnemyToAttack = false;
        pressingTheRangedAttackButtonIsPossibleAndCanThrowAxe = true;
        axeThrowHasBeenPressedOnceBeforeReturnJourney = false;
        colliderOfTheAxe.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetAxeThrowButtonInputOnlyForChangingState();
        if(StayWithThePlayerWhileAxeIsNotThrowing == true)
        {
            transform.position = transformOfThePlayer.position;
        }

        if (axeThrowScriptOnOffBoolean == true)
        {
            AxeThrowMainFunction();
        }
    }

    private void AxeThrowMainFunction()
    {
        CheckDistanceToPlayer();
        


        if (goToTheEnemyToAttack == true)
        {
            Vector3 directionToThePositionOfTheMouseClick = positionOfTheMouseClick - transformOfThePlayer.position;
            directionToThePositionOfTheMouseClick.Normalize();
            transform.Translate(directionToThePositionOfTheMouseClick * Time.deltaTime * speedOfAxeThrow);
            CheckIfAxeHasTravelledMaximumDistanceAndIfSoThenGoBackToPlayer();

        }

        else if (goBackToThePlayerAfterAttack == true)
        {
            GoBackToThePlayer();
            pressingTheRangedAttackButtonIsPossibleAndCanThrowAxe = false;
            CheckIfDistanceToPlayerHasReachedZeroAndIfSoThenReEnableAxeThrow();

        }

        if (pressingTheRangedAttackButtonIsPossibleAndCanThrowAxe == true)
        {
            if (axeThrowHasBeenPressedOnceBeforeReturnJourney == false && goBackToThePlayerAfterAttack == false)
            {
                DetectMousePositionWheneverFire2ButtonIsPressed();
            }

            else if (axeThrowHasBeenPressedOnceBeforeReturnJourney == true && goBackToThePlayerAfterAttack == false)
            {
                DetectRangedAttackButtonPressAndInstantlyTransportTheAxeBackToThePlayer();
            }
        }
    }


    private void GetAxeThrowButtonInputOnlyForChangingState()
    {
        if (Input.GetButtonDown("Fire2"))
        {

            playerStateControllerReference.ChangeStateAccordingToPriority(playerStateControllerReference.PLAYER_STATE_AXE_THROW);

        }
    }


    

    private void DetectMousePositionWheneverFire2ButtonIsPressed()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            StayWithThePlayerWhileAxeIsNotThrowing = false;
            positionOfTheMouseClick = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z);
            positionOfTheMouseClick = Camera.main.ScreenToWorldPoint(positionOfTheMouseClick);

            
            positionOfTheMouseClick.z = transform.position.z;
            
            goBackToThePlayerAfterAttack = false;
            goToTheEnemyToAttack = true;

            axeThrowHasBeenPressedOnceBeforeReturnJourney = true;

            colliderOfTheAxe.enabled = true;

        }
    }


   


    private void DetectRangedAttackButtonPressAndInstantlyTransportTheAxeBackToThePlayer()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            transform.position = transformOfThePlayer.position;
            StayWithThePlayerWhileAxeIsNotThrowing = true;
            axeThrowHasBeenPressedOnceBeforeReturnJourney = false;
            goBackToThePlayerAfterAttack = false;
            goToTheEnemyToAttack = false;
            colliderOfTheAxe.enabled = false;
            playerStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(playerStateControllerReference.PLAYER_STATE_AXE_THROW);
        }
    }

    private void CheckDistanceToPlayer()
    {
        directionToThePlayer = transformOfThePlayer.position - transformOfThisAxe.position;
        directionToThePlayerWithMagnitude = directionToThePlayer;
    }

    private void CheckIfDistanceToPlayerHasReachedZeroAndIfSoThenReEnableAxeThrow()
    {
        if (directionToThePlayerWithMagnitude.magnitude < 0.5)
        {
            pressingTheRangedAttackButtonIsPossibleAndCanThrowAxe = true;
            axeThrowHasBeenPressedOnceBeforeReturnJourney = false;
            goBackToThePlayerAfterAttack = false;
            goToTheEnemyToAttack = false;
            StayWithThePlayerWhileAxeIsNotThrowing = true;

            colliderOfTheAxe.enabled = false;

            playerStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(playerStateControllerReference.PLAYER_STATE_AXE_THROW);

        }
    }

    private void GoBackToThePlayer()
    {

        directionToThePlayer.Normalize();
        transform.Translate(directionToThePlayer * Time.deltaTime * speedOfAxeThrow);
    }

    private void CheckIfAxeHasTravelledMaximumDistanceAndIfSoThenGoBackToPlayer()
    {
        if (directionToThePlayerWithMagnitude.magnitude > maxDistanceOfAxeTravel)
        {
            goToTheEnemyToAttack = false;
            goBackToThePlayerAfterAttack = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            GoBackToThePlayerIntantlySinceTheAxeHasTouchedGroundLayer();
        }

        else if (collision.tag == "Enemy")
        {
            enemyRigidBody = collision.GetComponentInChildren<Rigidbody2D>();
            statusScriptOfTheEnemy = collision.GetComponentInChildren<Statuses>();
            statusScriptOfTheEnemy.DecreaseHealthByTheNumber(damageOfTheAxeThrow);
            statusScriptOfTheEnemy = null;
            enemyRigidBody = null;
        }
    }

    private void GoBackToThePlayerIntantlySinceTheAxeHasTouchedGroundLayer()
    {
        transform.position = transformOfThePlayer.position;
        StayWithThePlayerWhileAxeIsNotThrowing = true;
        axeThrowHasBeenPressedOnceBeforeReturnJourney = false;
        goBackToThePlayerAfterAttack = false;
        goToTheEnemyToAttack = false;
        colliderOfTheAxe.enabled = false;
        playerStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(playerStateControllerReference.PLAYER_STATE_AXE_THROW);
    }
}