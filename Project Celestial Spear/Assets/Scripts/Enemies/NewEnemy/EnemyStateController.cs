using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    public MovementControllerOfNewEnemy movementControllerOfTheNewEnemyScriptReference;
    public EnemyAttack enemyAttackSciptReference;
    public IntervalBetweenEnemyAttacksScript intervalBetweenEnemyAttackScriptReference;
    public EnemyGetsInterruptedUponPlayerAttack enemyGetsInterruptedUponPlayerAttackScriptReference;

    public string ENEMY_STATE_IDLE_OR_MOVING = "IdleOrMoving";
    public string ENEMY_STATE_ATTACKING = "EnemyAttacking";
    public string ENEMY_STATE_INTERVAL = "EnemyStateInterval";
    public string ENEMY_STATE_INTERRUPTED = "EnemyStateInterrupted";

    private int STATE_PRIORITY_ID_IDLE_OR_MOVING;
    private int STATE_PRIORITY_ID_ATTACKING;
    private int STATE_PRIORITY_ID_INTERVAL;
    private int STATE_PRIORITY_ID_INTERRUPTED;

    public bool isIdleOrMovingStateExecuting;
    public bool isAttackingStateExecuting;
    public bool isIntervalStateExecuting;
    public bool isInterruptedStateExecuting;

    public string currentState;
    private string defaultState;





    // Start is called before the first frame update
    void Start()
    {
        STATE_PRIORITY_ID_IDLE_OR_MOVING = 1;
        STATE_PRIORITY_ID_ATTACKING = 2;
        STATE_PRIORITY_ID_INTERVAL = 3;
        STATE_PRIORITY_ID_INTERRUPTED = 4;

        defaultState = ENEMY_STATE_IDLE_OR_MOVING;
        currentState = ENEMY_STATE_IDLE_OR_MOVING;
        TurnOnState(ENEMY_STATE_IDLE_OR_MOVING);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeStateAccordingToPriority(string newState)
    {
        if (currentState == newState)
        {
            return;
        }

        else
        {
            if (ReturnStateIdOfTheState(newState) > ReturnStateIdOfTheState(currentState))
            {
                StateExecutionHasCompletedAndTurnOnDefaultState(currentState);
                TurnOnState(newState);
                currentState = newState;
            }

            else if (ReturnStateIdOfTheState(newState) == ReturnStateIdOfTheState(currentState))
            {
                if (ReturnStateExecutionStatus(currentState) == false)
                {
                    StateExecutionHasCompletedAndTurnOnDefaultState(currentState);
                    TurnOnState(newState);
                    currentState = newState;
                }
            }

            else
            {
                return;
            }
        }
    }

    public void StateExecutionHasCompletedAndTurnOnDefaultState(string state)
    {
        if (state != currentState)
        {
            return;
        }

        else if (state == ENEMY_STATE_IDLE_OR_MOVING)
        {
            movementControllerOfTheNewEnemyScriptReference.movementControllerOfTheEnemyScriptControlBoolean = false;
            isIdleOrMovingStateExecuting = false;
            TurnOnState(defaultState);

        }

        else if (state == ENEMY_STATE_ATTACKING)
        {
            enemyAttackSciptReference.EnemyAttackScriptControlBoolean = false;
            enemyAttackSciptReference.CancelEnemyAttackIfAny();
            isAttackingStateExecuting = false;
            TurnOnState(defaultState);
        }

        else if(state == ENEMY_STATE_INTERVAL)
        {
            intervalBetweenEnemyAttackScriptReference.IntervalBetweenEnemyAttackScriptControlBoolean = false;
            isIntervalStateExecuting = false;
            TurnOnState(defaultState);
        }

        else if(state == ENEMY_STATE_INTERRUPTED)
        {
            enemyGetsInterruptedUponPlayerAttackScriptReference.enemyGetsInterruptedFromPlayerAttackScriptControlBoolean = false;
            isInterruptedStateExecuting = false;
            TurnOnState(defaultState);
        }


    }

    private void TurnOnState(string state)
    {
        movementControllerOfTheNewEnemyScriptReference.movementControllerOfTheEnemyScriptControlBoolean = false;
        enemyAttackSciptReference.EnemyAttackScriptControlBoolean = false;
        intervalBetweenEnemyAttackScriptReference.IntervalBetweenEnemyAttackScriptControlBoolean = false;
        enemyGetsInterruptedUponPlayerAttackScriptReference.enemyGetsInterruptedFromPlayerAttackScriptControlBoolean = false;

        isIdleOrMovingStateExecuting = false;
        isAttackingStateExecuting = false;
        isIntervalStateExecuting = false;
        isInterruptedStateExecuting = false;

        if(state == ENEMY_STATE_IDLE_OR_MOVING)
        {
            movementControllerOfTheNewEnemyScriptReference.movementControllerOfTheEnemyScriptControlBoolean = true;
            isIdleOrMovingStateExecuting = true;
            currentState = ENEMY_STATE_IDLE_OR_MOVING;
        }

        if(state == ENEMY_STATE_ATTACKING)
        {
            enemyAttackSciptReference.EnemyAttackScriptControlBoolean = true;
            isAttackingStateExecuting = true;
            currentState = ENEMY_STATE_ATTACKING;
        }

        if(state == ENEMY_STATE_INTERVAL)
        {
            intervalBetweenEnemyAttackScriptReference.IntervalBetweenEnemyAttackScriptControlBoolean = true;
            isIntervalStateExecuting = true;
            currentState = ENEMY_STATE_INTERVAL;
        }

        if(state == ENEMY_STATE_INTERRUPTED)
        {
            enemyGetsInterruptedUponPlayerAttackScriptReference.enemyGetsInterruptedFromPlayerAttackScriptControlBoolean = true;
            isInterruptedStateExecuting = true;
            currentState = ENEMY_STATE_INTERRUPTED;
        }

    }

    private bool ReturnStateExecutionStatus(string state)
    {
        if(state == ENEMY_STATE_IDLE_OR_MOVING)
        {
            return isIdleOrMovingStateExecuting;
        }

        if(state == ENEMY_STATE_ATTACKING)
        {
            return isAttackingStateExecuting;
        }

        if(state == ENEMY_STATE_INTERVAL)
        {
            return isIntervalStateExecuting;
        }

        if(state == ENEMY_STATE_INTERRUPTED)
        {
            return isInterruptedStateExecuting;
        }


        else
        {
            return false;
        }
    }

    private int ReturnStateIdOfTheState(string state)
    {
        if(state == ENEMY_STATE_IDLE_OR_MOVING)
        {
            return STATE_PRIORITY_ID_IDLE_OR_MOVING; 
        }

        if(state == ENEMY_STATE_ATTACKING)
        {
            return STATE_PRIORITY_ID_ATTACKING;
        }

        if(state == ENEMY_STATE_INTERVAL)
        {
            return STATE_PRIORITY_ID_INTERVAL;
        }

        if(state == ENEMY_STATE_INTERRUPTED)
        {
            return STATE_PRIORITY_ID_INTERRUPTED;
        }

        else
        {
            return -1;
        }
    }
}
