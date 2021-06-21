using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
    public float dodgeSpeed;
    public float thatMillisecondsOfTimeBeforeInvincibility;
    public float thatMillisecondsOfTimeOfTheInvincibilityPeriod;
    public float theDurationOfTheDodge;

    public Rigidbody2D myRigidbody2D;
    public BoxCollider2D myBoxCollider2D;
    public CircleCollider2D circleColliderForSmootheningWalking;
    public DodgeHitDetectionColliderScript dodgeHitDetectionColliderScriptReference;
    public PlayerAnimationController playerAnimationControllerScriptReference;
    public PlayerStateController playerStateControllerReference;

    public bool HoldBeforeTheDodge;
    public bool isInInvincibilityPeriod;
    public bool DoTheDodge;

    public bool dodgeScriptOnOffBoolean;

    public float playerFacingDirection;
    // Start is called before the first frame update
    void Start()
    {
       
        DoTheDodge = false;
        dodgeScriptOnOffBoolean = false;
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
            StartCoroutine(waitBeforeDodging());

            
        }


    }

    IEnumerator waitBeforeDodging()
    {
        

        HoldBeforeTheDodge = true;

        playerAnimationControllerScriptReference.ChangeAnimationState(playerAnimationControllerScriptReference.HOLD_THE_DODGE_ANIMATION);
        yield return new WaitForSeconds(thatMillisecondsOfTimeBeforeInvincibility);

        HoldBeforeTheDodge = false;

        isInInvincibilityPeriod = true;

        playerAnimationControllerScriptReference.ChangeAnimationState(playerAnimationControllerScriptReference.INVINCIBLE_STAGE_ANIMATION);
        dodgeHitDetectionColliderScriptReference.boxColliderForDetectingDodgeHits.enabled = true;
        yield return new WaitForSeconds(thatMillisecondsOfTimeOfTheInvincibilityPeriod);
        dodgeHitDetectionColliderScriptReference.boxColliderForDetectingDodgeHits.enabled = false;

        isInInvincibilityPeriod = false;

        if (DoTheDodge == false)
        {
            playerStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(playerStateControllerReference.PLAYER_STATE_DODGING);
        }

    }

    void CheckIfHitHasBeenDetectedDuringInvincibilityPhaseAndCallTheDodgeAppropriately()
    {
        if(isInInvincibilityPeriod == true && dodgeHitDetectionColliderScriptReference.hasAHitBeenDetectedDuringInvincibilityPhase == true)
        {
            StartCoroutine(IfHitHasBeenDetectedDuringTheInvincibilityPeriodThenAskToDoTheDodge());
            dodgeHitDetectionColliderScriptReference.boxColliderForDetectingDodgeHits.enabled = false;
            dodgeHitDetectionColliderScriptReference.hasAHitBeenDetectedDuringInvincibilityPhase = false;
        }
    }



    IEnumerator IfHitHasBeenDetectedDuringTheInvincibilityPeriodThenAskToDoTheDodge()
    {
        

        DoTheDodge = true;
        playerAnimationControllerScriptReference.ChangeAnimationState(playerAnimationControllerScriptReference.THE_ACTUAL_DODGE_ANIMATION);
        yield return new WaitForSeconds(theDurationOfTheDodge);
        DoTheDodge = false;
        FlipThePlayerToTheOppositeFacingSideAfterDodging();

        myBoxCollider2D.enabled = true;
        circleColliderForSmootheningWalking.enabled = true;
        myRigidbody2D.gravityScale = 1;

        playerStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(playerStateControllerReference.PLAYER_STATE_DODGING);
    }

    void TurnOffColliderAndGravityAndDoTheDodgeWheneverTheBooleanIsTrueAndDoTheOppositeWhenItIsFalse()
    {
        if (DoTheDodge == true)
        {
            myRigidbody2D.gravityScale = 0;
            myBoxCollider2D.enabled = false;
            circleColliderForSmootheningWalking.enabled = false;

            Vector2 forceToAddWhenDodging = new Vector2(dodgeSpeed * playerFacingDirection, 0f);
            myRigidbody2D.AddForce(forceToAddWhenDodging, ForceMode2D.Force);

            


        }

        if (DoTheDodge == false)
        {
            myBoxCollider2D.enabled = true;
            circleColliderForSmootheningWalking.enabled = true;
            myRigidbody2D.gravityScale = 1;
        }
    }


    void FlipThePlayerToTheOppositeFacingSideAfterDodging()
    {
        if (playerFacingDirection == -1)
        {
            gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);

        }

        else
        {
            gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);

        }
    }
}
