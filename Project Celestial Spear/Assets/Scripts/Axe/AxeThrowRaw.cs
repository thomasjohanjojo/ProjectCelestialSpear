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

    public bool axeThrowHasBeenPressedOnceBeforeReturnJourney = false;



    // Start is called before the first frame update
    void Start()
    {
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
        if (axeThrowScriptOnOffBoolean == true)
        {
            AxeThrowMainFunction();
        }
    }

    private void AxeThrowMainFunction()
    {
        CheckDistanceToPlayer();
        KeepRotationAsSame();


        if (goToTheEnemyToAttack == true)
        {
            transform.Translate(positionOfTheMouseClick * Time.deltaTime * speedOfAxeThrow);
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


    public void KeepRotationAsSame()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void DetectMousePositionWheneverFire2ButtonIsPressed()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            positionOfTheMouseClick = Input.mousePosition;
            positionOfTheMouseClick = Camera.main.ScreenToWorldPoint(positionOfTheMouseClick);
            positionOfTheMouseClick.z = transform.position.z;
            positionOfTheMouseClick.Normalize();
            goBackToThePlayerAfterAttack = false;
            goToTheEnemyToAttack = true;

            axeThrowHasBeenPressedOnceBeforeReturnJourney = true;

            colliderOfTheAxe.enabled = true;

        }
    }

    public void DetectRangedAttackButtonPressAndInstantlyTransportTheAxeBackToThePlayer()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            transform.position = transformOfThePlayer.position;
            axeThrowHasBeenPressedOnceBeforeReturnJourney = false;
            goBackToThePlayerAfterAttack = false;
            goToTheEnemyToAttack = false;
            colliderOfTheAxe.enabled = false;
            playerStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(playerStateControllerReference.PLAYER_STATE_AXE_THROW);
        }
    }

    public void CheckDistanceToPlayer()
    {
        directionToThePlayer = transformOfThePlayer.position - transformOfThisAxe.position;
        directionToThePlayerWithMagnitude = directionToThePlayer;
    }

    public void CheckIfDistanceToPlayerHasReachedZeroAndIfSoThenReEnableAxeThrow()
    {
        if (directionToThePlayerWithMagnitude.magnitude < 0.5)
        {
            pressingTheRangedAttackButtonIsPossibleAndCanThrowAxe = true;
            axeThrowHasBeenPressedOnceBeforeReturnJourney = false;
            goBackToThePlayerAfterAttack = false;
            goToTheEnemyToAttack = false;

            colliderOfTheAxe.enabled = false;

            playerStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(playerStateControllerReference.PLAYER_STATE_AXE_THROW);

        }
    }

    public void GoBackToThePlayer()
    {

        directionToThePlayer.Normalize();
        transform.Translate(directionToThePlayer * Time.deltaTime * speedOfAxeThrow);
    }

    public void CheckIfAxeHasTravelledMaximumDistanceAndIfSoThenGoBackToPlayer()
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

    public void GoBackToThePlayerIntantlySinceTheAxeHasTouchedGroundLayer()
    {
        transform.position = transformOfThePlayer.position;
        axeThrowHasBeenPressedOnceBeforeReturnJourney = false;
        goBackToThePlayerAfterAttack = false;
        goToTheEnemyToAttack = false;
        colliderOfTheAxe.enabled = false;
        playerStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(playerStateControllerReference.PLAYER_STATE_AXE_THROW);
    }
}