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

    public int damageOfTheAttack;

    public PlayerStateController playerStateControllerReference;

    private Rigidbody2D enemyRigidBody;

    private Statuses statusScriptOfTheEnemy;



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
        yield return new WaitForSeconds(durationOfTheAttack);

        verticalAttackArea.enabled = false;
        oneAttackIsExecutingRightNow = false;

        playerStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(playerStateControllerReference.PLAYER_STATE_AXE_THROW);
    }

    private IEnumerator DoTheHorizontalAttack()
    {
        oneAttackIsExecutingRightNow = true;
        horizontalAttackArea.enabled = true;
        yield return new WaitForSeconds(durationOfTheAttack);

        horizontalAttackArea.enabled = false;
        oneAttackIsExecutingRightNow = false;

        playerStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(playerStateControllerReference.PLAYER_STATE_AXE_THROW);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.tag == "Enemy")
        {
            enemyRigidBody = collision.GetComponentInChildren<Rigidbody2D>();
            statusScriptOfTheEnemy = collision.GetComponentInChildren<Statuses>();
            statusScriptOfTheEnemy.DecreaseHealthByTheNumber(damageOfTheAttack);
            statusScriptOfTheEnemy = null;
            enemyRigidBody = null;
        }
    }

}
