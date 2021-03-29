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
        obtainMovementInputFromPlayer();
        if (canMove == true)
        {
            flipPlayerFacingDirectionAccordingToDirectionOfInput();
            movePlayerHorizontally();
        }
        checkIfPlayerIsMoving();

        
    }

    void obtainMovementInputFromPlayer()
    {
        playerHorizontalInputValue = Input.GetAxisRaw("Horizontal");
    }

    void movePlayerHorizontally()
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

    void flipPlayerFacingDirectionAccordingToDirectionOfInput()
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

    void checkIfPlayerIsMoving()
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

    void changeCanMoveToAlternateBooleanValue()
    {
        canMove = !canMove;
    }

    

}
