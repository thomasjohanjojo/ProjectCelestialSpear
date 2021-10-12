using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetsInterruptedUponPlayerAttack : MonoBehaviour
{
    public EnemyAnimationCustomController enemyAnimationCustomControllerReference;

    public float durationOfTheInterruption;

    public EnemyStateController enemyStateControllerReference;

    public EnemyStatusScript enemyStatusScriptReference;

    private bool isOneInstanceOfTheCoroutineAlreadyRunning;

    public bool enemyGetsInterruptedFromPlayerAttackScriptControlBoolean;

    // Start is called before the first frame update
    void Start()
    {
        isOneInstanceOfTheCoroutineAlreadyRunning = false;
        enemyGetsInterruptedFromPlayerAttackScriptControlBoolean = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyStatusScriptReference.hasBeenInterrupted == true)
        {
            enemyStateControllerReference.ChangeStateAccordingToPriority(enemyStateControllerReference.ENEMY_STATE_INTERRUPTED);
            enemyStatusScriptReference.hasBeenInterrupted = false;
            
        }
        if (enemyGetsInterruptedFromPlayerAttackScriptControlBoolean == true)
        {
            MainFunction();
        }
            
    }

    private void MainFunction()
    {
        if (isOneInstanceOfTheCoroutineAlreadyRunning == false)
        {
            StartCoroutine(WaitInterruptedModeUntillTimerFinishes());
        }
    }

    private IEnumerator WaitInterruptedModeUntillTimerFinishes()
    {
        isOneInstanceOfTheCoroutineAlreadyRunning = true;
        enemyAnimationCustomControllerReference.ChangeAnimationState(enemyAnimationCustomControllerReference.ENEMY_IDLE_ANIMATION);
        yield return new WaitForSeconds(durationOfTheInterruption);
        enemyStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(enemyStateControllerReference.ENEMY_STATE_INTERRUPTED);
        isOneInstanceOfTheCoroutineAlreadyRunning = false;
    }
}
