using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public AttackRangeDetectionColliderScript attackRangeDetectionColliderScriptReference;
    public AttackGivingColliderScript attackGivingColliderScriptReference;
    public EnemyStateController enemyStateControllerReference;
    public FacingDirectionDeterminationScript facingDirectionDeterminationScriptReference;

    public EnemyAnimationCustomController enemyAnimationCustomControllerReference;
    public bool EnemyAttackScriptControlBoolean;

    public int damageOfTheAttack;

    public float pushBackForce;

    private bool oneInstanceOfTheCoRoutineIsRunningAlready;
    private bool doThePushBoolean;
    private Rigidbody2D rigidbodyOfThePlayerForPushing;

    
    

    // Start is called before the first frame update
    void Start()
    {
        oneInstanceOfTheCoRoutineIsRunningAlready = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if(attackRangeDetectionColliderScriptReference.playerIsInAttackRange == true)
        {
            enemyStateControllerReference.ChangeStateAccordingToPriority(enemyStateControllerReference.ENEMY_STATE_ATTACKING);
        }


        if(EnemyAttackScriptControlBoolean == true)
        {
            EnemyAttackMainFunction();
        }

        
    }


    private void FixedUpdate()
    {
        PushThePlayerInTheFixedUpdate();
    }


    private void EnemyAttackMainFunction()
    {
        if (oneInstanceOfTheCoRoutineIsRunningAlready == false)
        {
            StartCoroutine(AttackTheEnemyByTurningOnTheAnimationAndTheAttackColliders());
        }
    }
    
    private IEnumerator AttackTheEnemyByTurningOnTheAnimationAndTheAttackColliders()
    {
        
        oneInstanceOfTheCoRoutineIsRunningAlready = true;

        attackGivingColliderScriptReference.damageGivingBoxCollider.enabled = false;
        enemyAnimationCustomControllerReference.ChangeAnimationState(enemyAnimationCustomControllerReference.ENEMY_ATTACK_ANIMATION);
        yield return new WaitForSeconds(enemyAnimationCustomControllerReference.ENEMY_ATTACK_ANIMATION_DURATION);
        attackGivingColliderScriptReference.damageGivingBoxCollider.enabled = true;

        yield return new WaitForFixedUpdate();
        DamageThePlayerUsingTheCollider();
        yield return new WaitForFixedUpdate();

        attackGivingColliderScriptReference.damageGivingBoxCollider.enabled = false;
        

        enemyStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(enemyStateControllerReference.ENEMY_STATE_ATTACKING);
        enemyStateControllerReference.ChangeStateAccordingToPriority(enemyStateControllerReference.ENEMY_STATE_INTERVAL);


        oneInstanceOfTheCoRoutineIsRunningAlready = false;
        
        
    }
   

    private void DamageThePlayerUsingTheCollider()
    {
        if(attackGivingColliderScriptReference.gameObjectOfThePlayer)
        {
            if(attackGivingColliderScriptReference.statusScriptOfThePlayer)
            {

                
                attackGivingColliderScriptReference.statusScriptOfThePlayer.DecreaseHealthByTheNumber(damageOfTheAttack, attackGivingColliderScriptReference.damageGivingBoxCollider);
                PushThePlayer();
                attackGivingColliderScriptReference.statusScriptOfThePlayer = null;
                attackGivingColliderScriptReference.gameObjectOfThePlayer = null;
                
            }
        }

        
    }

    private void PushThePlayer()
    {
        if (attackGivingColliderScriptReference.gameObjectOfThePlayer)
        {
            if (attackGivingColliderScriptReference.statusScriptOfThePlayer)
            {

                if(attackGivingColliderScriptReference.statusScriptOfThePlayer.playerCanBeDamaged == true)
                {
                    rigidbodyOfThePlayerForPushing = attackGivingColliderScriptReference.gameObjectOfThePlayer.GetComponent<Rigidbody2D>();
                    DoThePushInTheFixedUpdateByTurningOnDoThePushBoolean();
                }
               

            }
        }
    }

    private void DoThePushInTheFixedUpdateByTurningOnDoThePushBoolean()
    {
        doThePushBoolean = true;
    }

    private void PushThePlayerInTheFixedUpdate()
    {
        if(doThePushBoolean == true)
        {
            if(rigidbodyOfThePlayerForPushing)
            {
                Vector2 pushBackForceToAddAsVector = new Vector2(facingDirectionDeterminationScriptReference.facingDirection * pushBackForce, 0f);
                rigidbodyOfThePlayerForPushing.AddForce(pushBackForceToAddAsVector, ForceMode2D.Impulse);
                doThePushBoolean = false;
                rigidbodyOfThePlayerForPushing = null;
            }
        }
    }



    public void CancelEnemyAttackIfAny()
    {
        ResetVariablesAndBooleans();
        StopAnyAttackRelatedCoroutines();
    }

    private void ResetVariablesAndBooleans()
    {
        attackGivingColliderScriptReference.damageGivingBoxCollider.enabled = false;
        oneInstanceOfTheCoRoutineIsRunningAlready = false;
        attackGivingColliderScriptReference.statusScriptOfThePlayer = null;
        attackGivingColliderScriptReference.gameObjectOfThePlayer = null;
    }

    private void StopAnyAttackRelatedCoroutines()
    {
        StopCoroutine(AttackTheEnemyByTurningOnTheAnimationAndTheAttackColliders());
    }
}
