using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrowScript : MonoBehaviour
{


    public BoxCollider2D verticalAttackArea;
    public BoxCollider2D horizontalAttackArea;

    public float durationOfTheAttack;

    public bool axeThrowScriptOnOffBoolean;

    public bool oneAttackIsExecutingRightNow;

    public AxeThrowAnimatorScript animatorScriptOfTheAxeThrowVertical;
    public AxeThrowAnimatorScript animatorScriptOfTheAxeThrowHorizontal;

    

    public PlayerStateController playerStateControllerReference;

    



    private void Start()
    {
        verticalAttackArea.enabled = false;
        horizontalAttackArea.enabled = false;
        oneAttackIsExecutingRightNow = false;
    }

    private void Update()
    {
        GetAxeThrowButtonInputOnlyForChangingState();
        if (axeThrowScriptOnOffBoolean == true)
        {
            if (oneAttackIsExecutingRightNow == false)
            {
                MainFunction();
            }

        }
    }

    private void MainFunction()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(DoTheVerticalAttack());
        }

        else if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(DoTheHorizontalAttack());
        }

    }

    private void GetAxeThrowButtonInputOnlyForChangingState()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
        {

            playerStateControllerReference.ChangeStateAccordingToPriority(playerStateControllerReference.PLAYER_STATE_AXE_THROW);

        }


        
        
    }


    private IEnumerator DoTheVerticalAttack()
    {
        oneAttackIsExecutingRightNow = true;
        verticalAttackArea.enabled = true;

        animatorScriptOfTheAxeThrowVertical.ChangeAnimationState(animatorScriptOfTheAxeThrowVertical.VERTICAL);
        yield return new WaitForSeconds(durationOfTheAttack);
        animatorScriptOfTheAxeThrowVertical.ChangeAnimationState(animatorScriptOfTheAxeThrowVertical.IDLE);

        verticalAttackArea.enabled = false;
        oneAttackIsExecutingRightNow = false;

        playerStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(playerStateControllerReference.PLAYER_STATE_AXE_THROW);
    }

    private IEnumerator DoTheHorizontalAttack()
    {
        oneAttackIsExecutingRightNow = true;
        horizontalAttackArea.enabled = true;

        animatorScriptOfTheAxeThrowHorizontal.ChangeAnimationState(animatorScriptOfTheAxeThrowHorizontal.HORIZONTAL);
        yield return new WaitForSeconds(durationOfTheAttack);
        animatorScriptOfTheAxeThrowHorizontal.ChangeAnimationState(animatorScriptOfTheAxeThrowHorizontal.IDLE);

        horizontalAttackArea.enabled = false;
        oneAttackIsExecutingRightNow = false;

        playerStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(playerStateControllerReference.PLAYER_STATE_AXE_THROW);
    }

    

}
