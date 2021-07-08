using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControllerOfNewEnemy : MonoBehaviour
{
    

    public DetectionRangeColliderScript detectionRangeColliderScriptReference;

    public EnemyAnimationCustomController enemyAnimationCustomControllerReference;

    public EnemyStateController enemyStateControllerScriptReference;

    public bool movementControllerOfTheEnemyScriptControlBoolean;

    public float movementSpeedOfTheEnemy;

    [SerializeField] private Rigidbody2D rigidbodyOfTheEnemy;

    private float facingDirectionOfTheEnemy;

    
    

    // Start is called before the first frame update
    void Start()
    {
        movementControllerOfTheEnemyScriptControlBoolean = true;
        facingDirectionOfTheEnemy = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (movementControllerOfTheEnemyScriptControlBoolean == true)
        {
            MainFunctionOfTheEnemyMovementController();
        }
    }

    private void MainFunctionOfTheEnemyMovementController()
    {
        if (detectionRangeColliderScriptReference.playerHasBeenDetectedInMovementRange == true)
        {
            FlipTheEnemyToFaceThePlayerIfNeeded();
            MoveTheEnemyToThePlayerWheneverHeIsDetected();
            enemyAnimationCustomControllerReference.ChangeAnimationState(enemyAnimationCustomControllerReference.ENEMY_WALK_ANIMATION);

        }

        else if (detectionRangeColliderScriptReference.playerHasBeenDetectedInMovementRange == false)
        {
            enemyAnimationCustomControllerReference.ChangeAnimationState(enemyAnimationCustomControllerReference.ENEMY_IDLE_ANIMATION);
        }
    }
    

    public void MoveTheEnemyToThePlayerWheneverHeIsDetected()
    {
        if (detectionRangeColliderScriptReference.gameObjectOfPlayerWhenHeGetsDetected)
        {
            Vector2 newPositionOfTheEnemy = new Vector2(transform.position.x + (facingDirectionOfTheEnemy * movementSpeedOfTheEnemy * Time.deltaTime), transform.position.y);
            rigidbodyOfTheEnemy.MovePosition(newPositionOfTheEnemy);
            
        }
    }


    public void FlipTheEnemyToFaceThePlayerIfNeeded()
    {
        if (detectionRangeColliderScriptReference.gameObjectOfPlayerWhenHeGetsDetected)
        {
            if (detectionRangeColliderScriptReference.gameObjectOfPlayerWhenHeGetsDetected.transform.position.x > gameObject.transform.position.x)
            {
                gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
                facingDirectionOfTheEnemy = 1;

            }

            else if (detectionRangeColliderScriptReference.gameObjectOfPlayerWhenHeGetsDetected.transform.position.x < gameObject.transform.position.x)
            {
                gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
                facingDirectionOfTheEnemy = -1;
            }
        }
    }
}
