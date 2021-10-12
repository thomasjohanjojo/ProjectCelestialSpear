using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrowRaw : MonoBehaviour
{
    public Transform transformOfThePlayer;

    public Vector3 directionToThePlayer;

    public Transform transformOfThisAxe;

    public Vector3 positionOfTheMouseClick;

    public PlayerAttack playerAttackScriptReferenceForGettingHitCounterValue;    

    public float maxDistanceOfAxeTravel;

    public float playerFacingDirectionToBeObtainedFromSomeOtherScript;

    public float speedOfAxeThrow;
    public float speedOfAxeThrowWhenItIsReturning;
    public int damageOfTheAxeThrow;

    public int amountToSubtractFromTheComboCounterAfterEachSuccesfulAxeThrow;
    private bool hasTheAxeDamagedAnyEnemiesInThisThrow;

    public BoxCollider2D colliderOfTheAxe;
     
    private Rigidbody2D enemyRigidBody;

    private EnemyStatusScript statusScriptOfTheEnemy;

    public PlayerStateController playerStateControllerReference;

    private Vector3 directionToThePlayerWithMagnitude;

    public bool axeThrowScriptOnOffBoolean;

    public bool goBackToThePlayerAfterAttack = false;
    public bool goToTheEnemyToAttack = false;
    public bool pressingTheRangedAttackButtonIsPossibleAndCanThrowAxe = true;

    private bool StayWithThePlayerWhileAxeIsNotThrowing;

    public bool axeThrowHasBeenPressedOnceBeforeReturnJourney = false;

    public bool canDetectInputFromPlayerControlBoolean;



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
        hasTheAxeDamagedAnyEnemiesInThisThrow = false;
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
            Vector3 directionToTravel = new Vector3(playerFacingDirectionToBeObtainedFromSomeOtherScript, 0, 0);
            transform.Translate(directionToTravel * Time.deltaTime * speedOfAxeThrow);
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
        if (Input.GetButtonDown("Fire2") && canDetectInputFromPlayerControlBoolean == true)
        {

            playerStateControllerReference.ChangeStateAccordingToPriority(playerStateControllerReference.PLAYER_STATE_AXE_THROW);

        }
    }

    private void DecreaseTheComboCounterNumberIfTheAxeHasDamagedAnyEnemiesInThisThrow()
    {
        if(hasTheAxeDamagedAnyEnemiesInThisThrow == true)
        {
            playerAttackScriptReferenceForGettingHitCounterValue.HitCounterInt = playerAttackScriptReferenceForGettingHitCounterValue.HitCounterInt - amountToSubtractFromTheComboCounterAfterEachSuccesfulAxeThrow;
            if(playerAttackScriptReferenceForGettingHitCounterValue.HitCounterInt <= 0)
            {
                playerAttackScriptReferenceForGettingHitCounterValue.HitCounterInt = 0;
            }
            hasTheAxeDamagedAnyEnemiesInThisThrow = false;
        }
    }
    

    private void DetectMousePositionWheneverFire2ButtonIsPressed()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            StayWithThePlayerWhileAxeIsNotThrowing = false;
            
            
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
            DecreaseTheComboCounterNumberIfTheAxeHasDamagedAnyEnemiesInThisThrow();
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

            DecreaseTheComboCounterNumberIfTheAxeHasDamagedAnyEnemiesInThisThrow();
            playerStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(playerStateControllerReference.PLAYER_STATE_AXE_THROW);

        }
    }

    private void GoBackToThePlayer()
    {

        directionToThePlayer.Normalize();
        transform.Translate(directionToThePlayer * Time.deltaTime * speedOfAxeThrowWhenItIsReturning);
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

        if (collision.tag == "Enemy")
        {
            hasTheAxeDamagedAnyEnemiesInThisThrow = true;
            enemyRigidBody = collision.GetComponentInChildren<Rigidbody2D>();
            statusScriptOfTheEnemy = collision.GetComponentInChildren<EnemyStatusScript>();
            statusScriptOfTheEnemy.DecreaseHealthByTheNumber(damageOfTheAxeThrow * playerAttackScriptReferenceForGettingHitCounterValue.HitCounterInt);
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
        DecreaseTheComboCounterNumberIfTheAxeHasDamagedAnyEnemiesInThisThrow();
        playerStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(playerStateControllerReference.PLAYER_STATE_AXE_THROW);
    }


    
}