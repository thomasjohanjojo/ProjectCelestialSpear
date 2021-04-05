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
    private float playerHorizontalInputValue;

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
        ObtainMovementInputFromPlayer();
        if (canMove == true)
        {
            doFlipPlayerFacingDirectionAccordingToDirectionOfInputFunctionInFixedUpdate = true;
            doMovePlayerHorizontallyFunctionInFixedUpdate = true;
        }
        CheckIfPlayerIsMoving();

        
    }

    private void FixedUpdate()
    {
        if (doFlipPlayerFacingDirectionAccordingToDirectionOfInputFunctionInFixedUpdate == true)
        {
            FlipPlayerFacingDirectionAccordingToDirectionOfInput();
            doFlipPlayerFacingDirectionAccordingToDirectionOfInputFunctionInFixedUpdate = false;
        }

        if (doMovePlayerHorizontallyFunctionInFixedUpdate == true)
        {
            MovePlayerHorizontally();
            doMovePlayerHorizontallyFunctionInFixedUpdate = false;
        }
    }

    void ObtainMovementInputFromPlayer()
    {
        playerHorizontalInputValue = Input.GetAxisRaw("Horizontal");
    }

    void MovePlayerHorizontally()
    {
        Vector2 forceToAddWhenMoving = new Vector2(playerHorizontalInputValue * playerSpeed, 0f);
        maincharacterRigidbody.AddForce(forceToAddWhenMoving, ForceMode2D.Impulse);

        if(playerHorizontalInputValue == 0)
        {
            maincharacterRigidbody.velocity = new Vector3(0f, maincharacterRigidbody.velocity.y, 0f);
        }

        if(Mathf.Abs( maincharacterRigidbody.velocity.x ) > playerSpeed)
        {
            if(playerHorizontalInputValue > 0)
            {
                maincharacterRigidbody.velocity = new Vector3(playerSpeed, maincharacterRigidbody.velocity.y, 0f);
            }

            else
            {
                maincharacterRigidbody.velocity = new Vector3(-playerSpeed, maincharacterRigidbody.velocity.y, 0f);
            }
        }
    }

    void FlipPlayerFacingDirectionAccordingToDirectionOfInput()
    {
        
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
            
        }

        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
           
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }

    }

    void CheckIfPlayerIsMoving()
    {
        if ((maincharacterRigidbody.velocity.x) != 0)
        {
            isMoving = true;
        }

        else
        {
            isMoving = false;
        }
    }


    //Functions to be used only in the future

    void ChangeCanMoveToAlternateBooleanValue()
    {
        canMove = !canMove;
    }

    

}
