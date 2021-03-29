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
        playerSpeed = 5;
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
        movePlayerHorizontally();
        
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
    }
}
