using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerStateController playerStateControllerReference;
    public MainCharacterBasicMovement playerControllerReferenceWhichHasATurnOnAndTurnOffBoolean;
    public PlayerAnimationController playerAnimationControllerReference;


    public AttackColliderScript attackColliderScriptReference;

    private Statuses statusSciptOfEnemy;
    private Rigidbody2D enemyRigidBody;


    private bool canAttack = true;
    public bool PlayerAttackScriptOnOffBoolean;
    public bool isAttacking;
    private int attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted = 10; // In case the user doesn't input a number. This represents the number of attacks, starting from zero
    public float playerFacingDirection;
    private bool doThePushBoolean;


    public float maximumAllowedDelayBetweenAttackButtonPresses = 1f;
    private float timeStampWhenAttackButtonWasLastPressed = 0f;


    public float pushBackForceOfFirstAttack;
    public int damageOfFirstAttack;
    public float windingUpTimeOfFirstAttack;
    public int damageOfSecondAttack;
    public float windingUpTimeOfSecondAttack;
    public int damageOfThirdAttack;
    public float windingUpTimeOfThirdAttack;
    public int damageOfFourthAttack;
    public float windingUpTimeOfFourthAttack;
    public int HitCounterInt = 0;


    // Start is called before the first frame update
    void Start()
    {

        PlayerAttackScriptOnOffBoolean = false;
        doThePushBoolean = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetAttackButtonInputForStateChanging();
        if (PlayerAttackScriptOnOffBoolean == true)
        {
            UpdateOrGrabEnemyRigidBodyFromAttackCollider();
            CheckPlayerFacingDirection();
            CheckIfPlayerCanAttackAndExecuteAttackIfThePlayerCan();
        }
    }

    private void FixedUpdate()
    {
        DoThePushInTheFixedUpdateWheneverTheDoThePushBooleanIsTrue();
    }


    private void GetAttackButtonInputForStateChanging()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) == true)
        {

            playerStateControllerReference.ChangeStateAccordingToPriority(playerStateControllerReference.PLAYER_STATE_ATTACKING);

        }


    }


    private void CheckIfPlayerCanAttackAndExecuteAttackIfThePlayerCan()
    {
        if (canAttack)
        {
            StartCoroutine(AttackWhenverAttackButtonIsPressedAndEnemyRigidbodyWithAnAttachedStatusScriptIsAvailable());
        }
    }


    public void SetCanAttackBooleanToTrueWhichShouldOnlyBeDoneThroughAxeThrowScript()
    {
        canAttack = true;
    }

    public void SetCanAttackBooleanToFalseWhichShouldOnlyBeDoneThroughAxeThrowScript()
    {
        canAttack = false;
    }






    public void IfEnemyHasBeenDetectedThenPushTheEnemy()
    {
        if (enemyRigidBody)
        {
            DoThePushThroughTheBooleanByTurningItOn();





        }


    }

    private void DoThePushThroughTheBooleanByTurningItOn()
    {
        doThePushBoolean = true;
    }

    private void DoThePushInTheFixedUpdateWheneverTheDoThePushBooleanIsTrue()
    {
        if (doThePushBoolean == true)
        {
            Vector2 pushBackForceToAddAsVector = new Vector2(playerFacingDirection * pushBackForceOfFirstAttack, 0f);
            enemyRigidBody.AddForce(pushBackForceToAddAsVector, ForceMode2D.Impulse);
            doThePushBoolean = false;
            enemyRigidBody = null;
        }
    }

    private void UpdateOrGrabEnemyRigidBodyFromAttackCollider()
    {
        if (attackColliderScriptReference.enemyRigidBody)
        {
            //Debug.Log("Entered in player script loop");
            HitCounterInt++;
            enemyRigidBody = attackColliderScriptReference.enemyRigidBody;
            statusSciptOfEnemy = attackColliderScriptReference.statusSciptOfEnemy;

        }

    }





    public void CheckPlayerFacingDirection()
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





    public IEnumerator AttackWhenverAttackButtonIsPressedAndEnemyRigidbodyWithAnAttachedStatusScriptIsAvailable()
    {



        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted += 1;

            ResetAttackIDCounterToZeroIfTooMuchDelayBetweenButtonPresses();


            if (attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted > 3)
            {
                attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted = 0;
            }


            if (attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted == 0)
            {
                SetMainCharacterVelocityToZeroToStopTheLeftOverMovementWhenCanMoveIsTurnedOff();

                canAttack = false;


                playerAnimationControllerReference.ChangeAnimationState(playerAnimationControllerReference.PUNCH_AND_PUSH_ANIMATION);
                yield return new WaitForSeconds(windingUpTimeOfFirstAttack);
                IfEnemyHasBeenDetectedThenPushTheEnemy();



                if (statusSciptOfEnemy)
                {
                    statusSciptOfEnemy.DecreaseHealthByTheNumber(damageOfFirstAttack);
                }

                timeStampWhenAttackButtonWasLastPressed = Time.time;

                statusSciptOfEnemy = null;

                canAttack = true;
            }

            if (attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted == 1)
            {
                SetMainCharacterVelocityToZeroToStopTheLeftOverMovementWhenCanMoveIsTurnedOff();
                canAttack = false;

                playerAnimationControllerReference.ChangeAnimationState(playerAnimationControllerReference.PLAYER_ATTACK_ONE_ANIMATION);
                yield return new WaitForSeconds(windingUpTimeOfSecondAttack);

                if (statusSciptOfEnemy)
                {
                    statusSciptOfEnemy.DecreaseHealthByTheNumber(damageOfSecondAttack);
                }

                timeStampWhenAttackButtonWasLastPressed = Time.time;

                statusSciptOfEnemy = null;

                canAttack = true;
            }

            if (attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted == 2)
            {
                SetMainCharacterVelocityToZeroToStopTheLeftOverMovementWhenCanMoveIsTurnedOff();
                canAttack = false;


                playerAnimationControllerReference.ChangeAnimationState(playerAnimationControllerReference.PLAYER_ATTACK_TWO_ANIMATION);
                yield return new WaitForSeconds(windingUpTimeOfThirdAttack);

                if (statusSciptOfEnemy)
                {
                    statusSciptOfEnemy.DecreaseHealthByTheNumber(damageOfThirdAttack);
                }

                timeStampWhenAttackButtonWasLastPressed = Time.time;



                statusSciptOfEnemy = null;

                canAttack = true;
            }


            if (attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted == 3)
            {
                SetMainCharacterVelocityToZeroToStopTheLeftOverMovementWhenCanMoveIsTurnedOff();
                canAttack = false;


                playerAnimationControllerReference.ChangeAnimationState(playerAnimationControllerReference.PLAYER_ATTACK_FINAL_ATTACK);
                yield return new WaitForSeconds(windingUpTimeOfFourthAttack);

                if (statusSciptOfEnemy)
                {
                    statusSciptOfEnemy.DecreaseHealthByTheNumber(damageOfFourthAttack);
                }

                timeStampWhenAttackButtonWasLastPressed = Time.time;



                statusSciptOfEnemy = null;

                canAttack = true;
            }


        }

        playerStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(playerStateControllerReference.PLAYER_STATE_ATTACKING);
    }

    private void ResetAttackIDCounterToZeroIfTooMuchDelayBetweenButtonPresses()
    {
        if (Time.time - timeStampWhenAttackButtonWasLastPressed > maximumAllowedDelayBetweenAttackButtonPresses)
        {
            attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted = 0;
        }
    }

    public void ResetAttackIDCounterToRightBeforeZeroWheneverRequired()
    {
        attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted = 10;
    }

    void SetMainCharacterVelocityToZeroToStopTheLeftOverMovementWhenCanMoveIsTurnedOff()
    {
        playerControllerReferenceWhichHasATurnOnAndTurnOffBoolean.SetMainCharacterVelocityToZeroWhenCalled();
    }
}
