using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{

    public AxeThrowScript axeThrowScriptReference;
    public PlayerAttack playerAttackScriptReference;
    public MainCharacterBasicMovement mainCharacterBasicMovementScriptReference;
    public PlayerDodge playerDodgeScriptReference;

    public string PLAYER_STATE_IDLE_OR_MOVING = "IdleOrMoving";
    public string PLAYER_STATE_ATTACKING = "Attacking";
    public string PLAYER_STATE_AXE_THROW = "AxeThrow";
    public string PLAYER_STATE_DODGING = "Dodging";

    private int STATE_PRIORITY_ID_IDLE_OR_MOVING;
    private int STATE_PRIORITY_ID_ATTACKING;
    private int STATE_PRIORITY_ID_AXE_THROW;
    private int STATE_PRIORITY_ID_DODGING;

    public bool isIdleOrMovingStateIsExecuting;
    public bool isAttackStateIsExecuting;
    public bool isAxeThrowStateIsExecuting;
    public bool isDodgeStateIsExecuting;


    public string currentState;
    string defaultState;

    

    // Start is called before the first frame update
    void Start()
    {
        STATE_PRIORITY_ID_IDLE_OR_MOVING = 1;
        STATE_PRIORITY_ID_ATTACKING = 2;
        STATE_PRIORITY_ID_AXE_THROW = 2;
        STATE_PRIORITY_ID_DODGING = 2;

        defaultState = PLAYER_STATE_IDLE_OR_MOVING;
        currentState = PLAYER_STATE_IDLE_OR_MOVING;
        TurnOnState(PLAYER_STATE_IDLE_OR_MOVING);
        
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
            if (ReturnTheStateIdOfTheState(newState) > ReturnTheStateIdOfTheState(currentState))
            {
                StateExecutionHasCompletedAndTurnOnDefaultState(currentState);
                TurnOnState(newState);
                currentState = newState;
            }

            else if (ReturnTheStateIdOfTheState(newState) == ReturnTheStateIdOfTheState(currentState))
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


    private void TurnOnState(string state)
    {
        mainCharacterBasicMovementScriptReference.mainCharacterBasicMovementScriptOnOffBoolean = false;
        playerAttackScriptReference.PlayerAttackScriptOnOffBoolean = false;
        axeThrowScriptReference.axeThrowScriptOnOffBoolean = false;
        playerDodgeScriptReference.dodgeScriptOnOffBoolean = false;

        isIdleOrMovingStateIsExecuting = false;
        isAttackStateIsExecuting = false;
        isAxeThrowStateIsExecuting = false;
        isDodgeStateIsExecuting = false;

        if (state == PLAYER_STATE_IDLE_OR_MOVING)
        {
            mainCharacterBasicMovementScriptReference.mainCharacterBasicMovementScriptOnOffBoolean = true;
            isIdleOrMovingStateIsExecuting = true;
            currentState = PLAYER_STATE_IDLE_OR_MOVING;
        }

        if (state == PLAYER_STATE_ATTACKING)
        {
            playerAttackScriptReference.PlayerAttackScriptOnOffBoolean = true;
            isAttackStateIsExecuting = true;
            currentState = PLAYER_STATE_ATTACKING;
        }

        if (state == PLAYER_STATE_AXE_THROW)
        {
            axeThrowScriptReference.axeThrowScriptOnOffBoolean = true;
            isAxeThrowStateIsExecuting = true;
            currentState = PLAYER_STATE_AXE_THROW;
        }

        if (state == PLAYER_STATE_DODGING)
        {
            playerDodgeScriptReference.dodgeScriptOnOffBoolean = true;
            isDodgeStateIsExecuting = true;
            currentState = PLAYER_STATE_DODGING;
        }

    }


    private bool ReturnStateExecutionStatus(string state)
    {
        if (state == PLAYER_STATE_IDLE_OR_MOVING)
        {
            return isIdleOrMovingStateIsExecuting;
        }

        if (state == PLAYER_STATE_ATTACKING)
        {
            return isAttackStateIsExecuting;
        }

        if (state == PLAYER_STATE_AXE_THROW)
        {
            return isAxeThrowStateIsExecuting;
        }

        if (state == PLAYER_STATE_DODGING)
        {
            return isDodgeStateIsExecuting;
        }

        else
        {
            return false;
        }
    }



    public void StateExecutionHasCompletedAndTurnOnDefaultState(string state)
    {

        if(state != currentState)
        {
            return;
        }

        else if (state == PLAYER_STATE_IDLE_OR_MOVING)
        {
            mainCharacterBasicMovementScriptReference.mainCharacterBasicMovementScriptOnOffBoolean = false;
            isIdleOrMovingStateIsExecuting = false;
            TurnOnState(defaultState);
            
        }

        else if (state == PLAYER_STATE_ATTACKING)
        {
            playerAttackScriptReference.PlayerAttackScriptOnOffBoolean = false;
            isAttackStateIsExecuting = false;
            TurnOnState(defaultState);
        }

        else if (state == PLAYER_STATE_AXE_THROW)
        {
            axeThrowScriptReference.axeThrowScriptOnOffBoolean = false;
            isAxeThrowStateIsExecuting = false;
            TurnOnState(defaultState);
        }

        else if (state == PLAYER_STATE_DODGING)
        {
            playerDodgeScriptReference.dodgeScriptOnOffBoolean = false;
            isDodgeStateIsExecuting = false;
            TurnOnState(defaultState);
        }
    }



    private int ReturnTheStateIdOfTheState(string state)
    {
        if(state == PLAYER_STATE_IDLE_OR_MOVING)
        {
            return STATE_PRIORITY_ID_IDLE_OR_MOVING;
        }

        if(state == PLAYER_STATE_ATTACKING)
        {
            return STATE_PRIORITY_ID_ATTACKING;
        }

        if(state == PLAYER_STATE_AXE_THROW)
        {
            return STATE_PRIORITY_ID_AXE_THROW;
        }

        if(state == PLAYER_STATE_DODGING)
        {
            return STATE_PRIORITY_ID_DODGING;
        }

        else
        {
            return 0;
        }
    }










   
}
