using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControllerOfNewEnemy : MonoBehaviour
{
    

    public DetectionRangeColliderScript detectionRangeColliderScriptReference;

    public EnemyAnimationCustomController enemyAnimationCustomControllerReference;

    public bool movementControllerOfTheEnemyScriptControlBoolean;

    

    // Start is called before the first frame update
    void Start()
    {
        movementControllerOfTheEnemyScriptControlBoolean = true;
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
            Vector2 positionOfPlayer = new Vector2(detectionRangeColliderScriptReference.gameObjectOfPlayerWhenHeGetsDetected.transform.position.x, detectionRangeColliderScriptReference.gameObjectOfPlayerWhenHeGetsDetected.transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, positionOfPlayer, 1f * Time.deltaTime);
        }
    }


    public void FlipTheEnemyToFaceThePlayerIfNeeded()
    {
        if (detectionRangeColliderScriptReference.gameObjectOfPlayerWhenHeGetsDetected)
        {
            if (detectionRangeColliderScriptReference.gameObjectOfPlayerWhenHeGetsDetected.transform.position.x > gameObject.transform.position.x)
            {
                gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);

            }

            else if (detectionRangeColliderScriptReference.gameObjectOfPlayerWhenHeGetsDetected.transform.position.x < gameObject.transform.position.x)
            {
                gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
            }
        }
    }
}
