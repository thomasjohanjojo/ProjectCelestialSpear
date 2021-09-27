using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
    public float dodgeSpeed;
    public float thatMillisecondsOfTimeBeforeInvincibility;
    public float thatMillisecondsOfTimeOfTheInvincibilityPeriod;
    public float theDurationOfTheDodge;
    public Statuses playerStatusScript;
    public PlayerAttack playerAttackScriptForMaintainingTheComboCounterNumberInCaseOfASuccessfulDodge;
    private int comboCounterNumberToRestoreTheComboCounterNumberAfterASuccesfulDodge;

    public bool dodgeHasBeenPerformedAnnoucerBoolean;

    public Rigidbody2D myRigidbody2D;
    public BoxCollider2D myBoxCollider2D;
    
    public DodgeHitDetectionColliderScript dodgeHitDetectionColliderScriptReference;
    public PlayerAnimationController playerAnimationControllerScriptReference;
    public PlayerStateController playerStateControllerReference;
    public MainCharacterBasicMovement mainCharacterBasicMovementScriptForEnsuringMeleeComboContinuationAfterDodging;

    private bool HoldBeforeTheDodge;
    private bool isInInvincibilityPeriod;
    private bool DoTheDodge;


    private bool oneInstanceOfWaitBeforeDodgingCoroutineIsAlreadyExecuting;
    private bool oneInstanceOfHitHasBeenDetectedDuringInvincibilityCoroutineIsAlreadyExecuting;

    public bool dodgeScriptOnOffBoolean;

    private float playerFacingDirection;
    // Start is called before the first frame update
    void Start()
    {
        dodgeHasBeenPerformedAnnoucerBoolean = false;
        DoTheDodge = false;
        dodgeScriptOnOffBoolean = false;
        oneInstanceOfWaitBeforeDodgingCoroutineIsAlreadyExecuting = false;
        oneInstanceOfHitHasBeenDetectedDuringInvincibilityCoroutineIsAlreadyExecuting = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfDodgeButtonHasPressedForChangingState();
        if (dodgeScriptOnOffBoolean == true)
        {
            CheckPlayerFacingDirection();
            DetectWhenShiftButtonIsPressedThenStartTheTimerAndAlsoStopItAfterSomeTimeByTurningTheBooleanOnAndOff();
            CheckIfHitHasBeenDetectedDuringInvincibilityPhaseAndCallTheDodgeAppropriately();
            TurnOffColliderAndGravityAndDoTheDodgeWheneverTheBooleanIsTrueAndDoTheOppositeWhenItIsFalse();

        }


    }


    void CheckIfDodgeButtonHasPressedForChangingState()
    {
       if( Input.GetKeyDown((KeyCode.LeftShift)) == true)
        {
            playerStateControllerReference.ChangeStateAccordingToPriority(playerStateControllerReference.PLAYER_STATE_DODGING);
        }
    }

    void CheckPlayerFacingDirection()
    {
        if (gameObject.transform.rotation.y == 0)
        {
            playerFacingDirection = 1; // 1 means right

        }
        else if (gameObject.transform.rotation.y == -1)
        {
            playerFacingDirection = -1;

        }
    }


    void DetectWhenShiftButtonIsPressedThenStartTheTimerAndAlsoStopItAfterSomeTimeByTurningTheBooleanOnAndOff()
    {

        if (Input.GetKeyDown((KeyCode.LeftShift)) == true)
        {
            if (oneInstanceOfWaitBeforeDodgingCoroutineIsAlreadyExecuting == false)
            {
                StartCoroutine(waitBeforeDodging());
            }
            
        }


    }

    IEnumerator waitBeforeDodging()
    {

        oneInstanceOfWaitBeforeDodgingCoroutineIsAlreadyExecuting = true;        

        HoldBeforeTheDodge = true;
        comboCounterNumberToRestoreTheComboCounterNumberAfterASuccesfulDodge = playerAttackScriptForMaintainingTheComboCounterNumberInCaseOfASuccessfulDodge.HitCounterInt;

        playerAnimationControllerScriptReference.ChangeAnimationState(playerAnimationControllerScriptReference.HOLD_THE_DODGE_ANIMATION);
        yield return new WaitForSeconds(thatMillisecondsOfTimeBeforeInvincibility);

        HoldBeforeTheDodge = false;

        isInInvincibilityPeriod = true;

        playerAnimationControllerScriptReference.ChangeAnimationState(playerAnimationControllerScriptReference.INVINCIBLE_STAGE_ANIMATION);
        playerStatusScript.playerCanBeDamaged = false;
        dodgeHitDetectionColliderScriptReference.boxColliderForDetectingDodgeHits.enabled = true;
        yield return new WaitForSeconds(thatMillisecondsOfTimeOfTheInvincibilityPeriod);
        dodgeHitDetectionColliderScriptReference.boxColliderForDetectingDodgeHits.enabled = false;
        playerStatusScript.playerCanBeDamaged = true;

        isInInvincibilityPeriod = false;

        if (DoTheDodge == false)
        {
            comboCounterNumberToRestoreTheComboCounterNumberAfterASuccesfulDodge = 0;
            playerStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(playerStateControllerReference.PLAYER_STATE_DODGING);
        }

        oneInstanceOfWaitBeforeDodgingCoroutineIsAlreadyExecuting = false;

    }

    void CheckIfHitHasBeenDetectedDuringInvincibilityPhaseAndCallTheDodgeAppropriately()
    {
        if(isInInvincibilityPeriod == true && dodgeHitDetectionColliderScriptReference.hasAHitBeenDetectedDuringInvincibilityPhase == true)
        {
            if (oneInstanceOfHitHasBeenDetectedDuringInvincibilityCoroutineIsAlreadyExecuting == false)
            {
                StartCoroutine(IfHitHasBeenDetectedDuringTheInvincibilityPeriodThenAskToDoTheDodge());
                dodgeHitDetectionColliderScriptReference.boxColliderForDetectingDodgeHits.enabled = false;
                dodgeHitDetectionColliderScriptReference.hasAHitBeenDetectedDuringInvincibilityPhase = false;
            }
        }
    }



    IEnumerator IfHitHasBeenDetectedDuringTheInvincibilityPeriodThenAskToDoTheDodge()
    {

        oneInstanceOfHitHasBeenDetectedDuringInvincibilityCoroutineIsAlreadyExecuting = true;

        DoTheDodge = true;
        playerAnimationControllerScriptReference.ChangeAnimationState(playerAnimationControllerScriptReference.THE_ACTUAL_DODGE_ANIMATION);
        yield return new WaitForSeconds(theDurationOfTheDodge);
        DoTheDodge = false;
        FlipThePlayerToTheOppositeFacingSideAfterDodging();
        
        playerAttackScriptForMaintainingTheComboCounterNumberInCaseOfASuccessfulDodge.HitCounterInt = comboCounterNumberToRestoreTheComboCounterNumberAfterASuccesfulDodge;

        myBoxCollider2D.enabled = true;
        
        myRigidbody2D.gravityScale = 1;

        playerStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(playerStateControllerReference.PLAYER_STATE_DODGING);

        oneInstanceOfHitHasBeenDetectedDuringInvincibilityCoroutineIsAlreadyExecuting = false;
    }

    void TurnOffColliderAndGravityAndDoTheDodgeWheneverTheBooleanIsTrueAndDoTheOppositeWhenItIsFalse()
    {
        if (DoTheDodge == true)
        {
            myRigidbody2D.gravityScale = 0;
            myBoxCollider2D.enabled = false;
            

            Vector2 forceToAddWhenDodging = new Vector2(dodgeSpeed * playerFacingDirection, 0f);
            myRigidbody2D.AddForce(forceToAddWhenDodging, ForceMode2D.Force);

            dodgeHasBeenPerformedAnnoucerBoolean = true;
            
            

            


        }

        if (DoTheDodge == false)
        {
            myBoxCollider2D.enabled = true;
           
            myRigidbody2D.gravityScale = 1;
        }
    }


    void FlipThePlayerToTheOppositeFacingSideAfterDodging()
    {
        if (playerFacingDirection == -1)
        {
            gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
            mainCharacterBasicMovementScriptForEnsuringMeleeComboContinuationAfterDodging.lastFacingDirection = 1;
            mainCharacterBasicMovementScriptForEnsuringMeleeComboContinuationAfterDodging.currentFacingDirection = 1;

        }

        else
        {
            gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
            mainCharacterBasicMovementScriptForEnsuringMeleeComboContinuationAfterDodging.lastFacingDirection = -1;
            mainCharacterBasicMovementScriptForEnsuringMeleeComboContinuationAfterDodging.currentFacingDirection = -1;

        }
    }
}
