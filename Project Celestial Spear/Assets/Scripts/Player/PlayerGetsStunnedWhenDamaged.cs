using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetsStunnedWhenDamaged : MonoBehaviour
{

    public PlayerStateController playerStateControllerScriptReference;
    public PlayerAnimationController playerAnimationControllerScriptReference;
    public Statuses playerStatusScriptReference;
        
    public float durationOfBeingStunned;

    private bool isOneInstanceOfTheCoroutineAlreadyRunning;

    public bool playerGetsStunnedWhenDamagedScriptControlBoolean;

    // Start is called before the first frame update
    void Start()
    {
        isOneInstanceOfTheCoroutineAlreadyRunning = false;
        playerGetsStunnedWhenDamagedScriptControlBoolean = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStatusScriptReference.playerHasBeenStunned == true)
        {
            playerStateControllerScriptReference.ChangeStateAccordingToPriority(playerStateControllerScriptReference.PLAYER_STATE_STUNNED);
            playerStatusScriptReference.playerHasBeenStunned = false;
        }

        if(playerGetsStunnedWhenDamagedScriptControlBoolean == true)
        {
            MainFunction();
        }
    }

    private void MainFunction()
    {
        if(isOneInstanceOfTheCoroutineAlreadyRunning == false)
        {
            StartCoroutine(WaitInStunnedModeUntillTimerFinishes());
        }
    }

    private IEnumerator WaitInStunnedModeUntillTimerFinishes()
    {
        isOneInstanceOfTheCoroutineAlreadyRunning = true;
        playerAnimationControllerScriptReference.ChangeAnimationState(playerAnimationControllerScriptReference.IDLE_ANIMATION);
        yield return new WaitForSeconds(durationOfBeingStunned);
        playerStateControllerScriptReference.StateExecutionHasCompletedAndTurnOnDefaultState(playerStateControllerScriptReference.PLAYER_STATE_STUNNED);
        isOneInstanceOfTheCoroutineAlreadyRunning = false;
    }
}
