using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrowScript : MonoBehaviour
{

    public Transform transformOfThePlayer;

    public Vector3 directionToThePlayer;

    public Transform transformOfThisAxe;

    public Vector3 positionOfTheMouseClick;

    public float maxDistanceOfAxeTravel;

    public float speedOfAxeThrow;

    public int damageOfTheAxeThrow;

    public BoxCollider2D colliderOfTheAxe;

    private Vector3 directionToThePlayerWithMagnitude;

    private Statuses statusScriptOfTheEnemy;

    public PlayerAnimationController animationControllerOfThePlayer;

    public MainCharacterBasicMovement mainCharacterBasicMovementScriptReference;
    public PlayerAttack playerAttackScriptReference;

    

    private Rigidbody2D enemyRigidBody;

    public bool isAxeThowing;

    public bool goBackToThePlayerAfterAttack = false;
    public bool goToTheEnemyToAttack = false;
    public bool pressingTheRangedAttackButtonIsPossibleAndCanThrowAxe = true;

    public bool axeThrowHasBeenPressedOnceBeforeReturnJourney = false;

    public bool canPlayAxeReapppearAnimationJustOnce = false;

    public bool axeHasReachedThePlayerOnce;



    // Start is called before the first frame update
    void Start()
    {
        goBackToThePlayerAfterAttack = false;
        goToTheEnemyToAttack = false;
        pressingTheRangedAttackButtonIsPossibleAndCanThrowAxe = true;
        axeThrowHasBeenPressedOnceBeforeReturnJourney = false;
        colliderOfTheAxe.enabled = false;
        canPlayAxeReapppearAnimationJustOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceToPlayer();

        if(goToTheEnemyToAttack == true)
        {
            transform.Translate(positionOfTheMouseClick * Time.deltaTime * speedOfAxeThrow);
            CheckIfAxeHasTravelledMaximumDistanceAndIfSoThenGoBackToPlayer();
            
        }

        else if(goBackToThePlayerAfterAttack == true)
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

            else if(axeThrowHasBeenPressedOnceBeforeReturnJourney == true && goBackToThePlayerAfterAttack == false)
            {
                DetectRangedAttackButtonPressAndInstantlyTransportTheAxeBackToThePlayer();
            }
        }
    }


   

    public void TurnOnOtherPlayerStates()
    {
        playerAttackScriptReference.SetCanAttackBooleanToTrueWhichShouldOnlyBeDoneThroughAxeThrowScript();
        mainCharacterBasicMovementScriptReference.canMove = true;
    }

    public void TurnOffOtherPlayerStates()
    {
        playerAttackScriptReference.SetCanAttackBooleanToFalseWhichShouldOnlyBeDoneThroughAxeThrowScript();
        mainCharacterBasicMovementScriptReference.canMove = false;
    }

    public void DetectMousePositionWheneverFire2ButtonIsPressed()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            positionOfTheMouseClick = Input.mousePosition;
            positionOfTheMouseClick = Camera.main.ScreenToWorldPoint(positionOfTheMouseClick);
            positionOfTheMouseClick.z = transform.position.z;
            positionOfTheMouseClick.Normalize();
            goBackToThePlayerAfterAttack = false;
            goToTheEnemyToAttack = true;

            isAxeThowing = true;

            axeHasReachedThePlayerOnce = false;
            axeThrowHasBeenPressedOnceBeforeReturnJourney = true;
            TurnOffOtherPlayerStates();

            colliderOfTheAxe.enabled = true;
            
        }
    }

    public void DetectRangedAttackButtonPressAndInstantlyTransportTheAxeBackToThePlayer()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            transform.position = transformOfThePlayer.position;
            axeThrowHasBeenPressedOnceBeforeReturnJourney = false;
            goBackToThePlayerAfterAttack = false;
            goToTheEnemyToAttack = false;
            colliderOfTheAxe.enabled = false;

            isAxeThowing = false;
            axeHasReachedThePlayerOnce = true;
            TurnOnOtherPlayerStates();

            
        }
    }

    public void CheckDistanceToPlayer()
    {
        directionToThePlayer = transformOfThePlayer.position - transformOfThisAxe.position;
        directionToThePlayerWithMagnitude = directionToThePlayer;
    }

    public void CheckIfDistanceToPlayerHasReachedZeroAndIfSoThenReEnableAxeThrow()
    {
        if(directionToThePlayerWithMagnitude.magnitude < 0.5)
        {
            if (axeHasReachedThePlayerOnce == false)
            {
                pressingTheRangedAttackButtonIsPossibleAndCanThrowAxe = true;
                axeThrowHasBeenPressedOnceBeforeReturnJourney = false;
                goBackToThePlayerAfterAttack = false;
                goToTheEnemyToAttack = false;


                isAxeThowing = false;
                colliderOfTheAxe.enabled = false;

                axeHasReachedThePlayerOnce = true;
                TurnOnOtherPlayerStates();

            }

            

            
            
        }
    }

    public void PlayAxeReappearToHandAnimationOnlyOnce()
    {
        if (canPlayAxeReapppearAnimationJustOnce)
        {
            animationControllerOfThePlayer.ChangeAnimationState(animationControllerOfThePlayer.AXE_REAPPEAR_TO_HAND_ANIMATION);
            canPlayAxeReapppearAnimationJustOnce = false;
        }
    }

    public void GoBackToThePlayer()
    {
        
        directionToThePlayer.Normalize();
        transform.Translate(directionToThePlayer * Time.deltaTime * speedOfAxeThrow);

        
    }

    public void CheckIfAxeHasTravelledMaximumDistanceAndIfSoThenGoBackToPlayer()
    {
        if(directionToThePlayerWithMagnitude.magnitude > maxDistanceOfAxeTravel)
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

        else if(collision.tag == "Enemy")
        {
            enemyRigidBody = collision.GetComponent<Rigidbody2D>();
            statusScriptOfTheEnemy = collision.GetComponent<Statuses>();
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

        
    }
}
