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
    private float playerInputValue; 








    // Start is called before the first frame update
    void Start()
    {
        //Default Values
        playerSpeed = 5;
        canMove = true;
        isMoving = false;
        playerInputValue = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
