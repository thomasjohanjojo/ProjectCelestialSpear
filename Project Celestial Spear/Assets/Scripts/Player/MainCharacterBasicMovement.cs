using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterBasicMovement : MonoBehaviour
{
    /// <summary>
    /// Basic player movement for moving left and right at a moderate speed with other variables for increased manipulation
    /// </summary>

    //Fields
    public float playerSpeed;
    public bool canMove;
    public bool isMoving;
    public bool mainCharacterBasicMovementScriptOnOffBoolean;
    private float playerHorizontalInputValue;
    public  int lastFacingDirection = 1;
    public int currentFacingDirection = 1;

    public PlayerAnimationController playerAnimationControllerReference;
    public PlayerAttack playerAttackScriptReference;
    public PlayerStateController playerStateControllerReference;
    public ObstructionDetectionColliderScriptForPlayer obstructionDetectionColliderScriptReference;
    Rigidbody2D maincharacterRigidbody;
   


    //Booleans for communicating with update and fixed update. The input from player will be recieved in update and it will be implemented in physics engine through fixed update. Therefore, whenever an input is recieved in update, a signal is given through a boolean and the fixed update will implement the funcitonality

    bool doFlipPlayerFacingDirectionAccordingToDirectionOfInputFunctionInFixedUpdate;
    bool doMovePlayerHorizontallyFunctionInFixedUpdate;
    




    // Start is called before the first frame update
    void Start()
    {
        //Default Values
        
        canMove = true;
        isMoving = false;
        playerHorizontalInputValue = 0;

        //Obtaining references to rigidbody
        maincharacterRigidbody = gameObject.GetComponent<Rigidbody2D>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCharacterBasicMovementScriptOnOffBoolean == true)
        {
            ObtainMovementInputFromPlayer();
            if (canMove == true)
            {
                doFlipPlayerFacingDirectionAccordingToDirectionOfInputFunctionInFixedUpdate = true;
                              
                doMovePlayerHorizontallyFunctionInFixedUpdate = true;
                                              
                CheckIfPlayerIsMovingAndCallTheAppropriateMovementAnimation();
            }

            playerStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(playerStateControllerReference.PLAYER_STATE_IDLE_OR_MOVING);
        }
        
    }

    private void FixedUpdate()
    {
        if (mainCharacterBasicMovementScriptOnOffBoolean == true)
        {
            if (doFlipPlayerFacingDirectionAccordingToDirectionOfInputFunctionInFixedUpdate == true)
            {
                FlipPlayerFacingDirectionAccordingToDirectionOfInput();
                doFlipPlayerFacingDirectionAccordingToDirectionOfInputFunctionInFixedUpdate = false;
            }

            if (doMovePlayerHorizontallyFunctionInFixedUpdate == true)
            {
                if (obstructionDetectionColliderScriptReference.ObstructionInFront == false)
                {
                    MoveRigidBodyHorizontallyOnTheBasisOfInputAndMovementSpeedRequired(maincharacterRigidbody, playerHorizontalInputValue, playerSpeed);
                }
                else
                {
                    maincharacterRigidbody.velocity = new Vector3(0f, maincharacterRigidbody.velocity.y, 0f);
                }

                doMovePlayerHorizontallyFunctionInFixedUpdate = false;
            }
        }
    }

    void ObtainMovementInputFromPlayer()
    {
        playerHorizontalInputValue = Input.GetAxisRaw("Horizontal");
    }






    public void MoveRigidBodyHorizontallyOnTheBasisOfInputAndMovementSpeedRequired(Rigidbody2D rigidbody, float horizontalInputValue, float maximumMovementSpeed)
    {
        AddForceToTheRigidBodyOnTheBasisOfHorizontalInputValueAndMaxMovementSpeed(rigidbody, horizontalInputValue, maximumMovementSpeed);
        BringTheSpeedOfTheRigidbodyToZeroIfThereIsNoHorizontalInput(rigidbody, horizontalInputValue);
        LimitTheSpeedOfTheBodyToMaximumSpeedWhenThereIsInputAndTheAmountOfForceExceedsMaximumSpeed(rigidbody, horizontalInputValue, maximumMovementSpeed);
    }


    private void LimitTheSpeedOfTheBodyToMaximumSpeedWhenThereIsInputAndTheAmountOfForceExceedsMaximumSpeed(Rigidbody2D rigidbody, float horizontalInputValue, float maximumMovementSpeed)
    {
        if (Mathf.Abs(rigidbody.velocity.x) > maximumMovementSpeed)
        {
            if (horizontalInputValue > 0)
            {
                rigidbody.velocity = new Vector3(maximumMovementSpeed, rigidbody.velocity.y, 0f);
            }

            else
            {
                rigidbody.velocity = new Vector3(-maximumMovementSpeed, rigidbody.velocity.y, 0f);
            }
        }
    }

    private  void BringTheSpeedOfTheRigidbodyToZeroIfThereIsNoHorizontalInput(Rigidbody2D rigidbody, float horizontalInputValue)
    {
        if (horizontalInputValue == 0)
        {
            rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, 0f);
        }
    }

    private void AddForceToTheRigidBodyOnTheBasisOfHorizontalInputValueAndMaxMovementSpeed(Rigidbody2D rigidbody,float horizontalInputValue, float maximumMovementSpeed)
    {
        Vector2 forceToAddWhenMoving = new Vector2(horizontalInputValue * maximumMovementSpeed, 0f);
        rigidbody.AddForce(forceToAddWhenMoving, ForceMode2D.Impulse);
    }






    void FlipPlayerFacingDirectionAccordingToDirectionOfInput()
    {
        
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
            currentFacingDirection = 1;
            ResetTheAttackIfThePlayerHasTrulyBeenFlippedBasedOnTheCurrentFacingDirectionAndTheLastFacingDirection();

            
            
        }

        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
            currentFacingDirection = -1;
            ResetTheAttackIfThePlayerHasTrulyBeenFlippedBasedOnTheCurrentFacingDirectionAndTheLastFacingDirection();

            
           
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }

    }

    void CheckIfPlayerIsMovingAndCallTheAppropriateMovementAnimation()
    {
        if ((maincharacterRigidbody.velocity.x) != 0 && (Input.GetAxisRaw("Horizontal") != 0))
        {
            isMoving = true;
            playerAnimationControllerReference.ChangeAnimationState(playerAnimationControllerReference.RUNNING_ANIMATION);
            
        }

        else
        {
            isMoving = false;
            playerAnimationControllerReference.ChangeAnimationState(playerAnimationControllerReference.IDLE_ANIMATION);
            
            
        }
    }


    void ResetTheAttackIfThePlayerHasTrulyBeenFlippedBasedOnTheCurrentFacingDirectionAndTheLastFacingDirection()
    {
        if(currentFacingDirection != lastFacingDirection)
        {
            lastFacingDirection = currentFacingDirection;
            playerAttackScriptReference.ResetAttackIDCounterToRightBeforeZeroWheneverRequired();
            
        }
    }

    void ChangeCanMoveToAlternateBooleanValue()
    {
        canMove = !canMove;
    }

    public void SetMainCharacterVelocityToZeroWhenCalled()
    {
        maincharacterRigidbody.velocity = new Vector3(0f, maincharacterRigidbody.velocity.y, 0f);
    }

    

}