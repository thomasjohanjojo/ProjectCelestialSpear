using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalBetweenEnemyAttacksScript : MonoBehaviour
{

    public EnemyAnimationCustomController enemyAnimationCustomControllerReference;

    public float durationOfTheInterval;

    public EnemyStateController enemyStateControllerReference;

    private bool isOneInstanceOfTheCoroutineAlreadyRunning;

    public bool IntervalBetweenEnemyAttackScriptControlBoolean;

    // Start is called before the first frame update
    void Start()
    {
        isOneInstanceOfTheCoroutineAlreadyRunning = false;
        IntervalBetweenEnemyAttackScriptControlBoolean = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(IntervalBetweenEnemyAttackScriptControlBoolean == true)
        {
            MainFunction();
        }
    }

    private void MainFunction()
    {
        if(isOneInstanceOfTheCoroutineAlreadyRunning == false)
        {
            StartCoroutine(WaitInIdleModeUntillTimerFinishes());
        }
    }

    private IEnumerator WaitInIdleModeUntillTimerFinishes()
    {
        isOneInstanceOfTheCoroutineAlreadyRunning = true;
        enemyAnimationCustomControllerReference.ChangeAnimationState(enemyAnimationCustomControllerReference.ENEMY_IDLE_ANIMATION);
        yield return new WaitForSeconds(durationOfTheInterval);
        enemyStateControllerReference.StateExecutionHasCompletedAndTurnOnDefaultState(enemyStateControllerReference.ENEMY_STATE_INTERVAL);
        isOneInstanceOfTheCoroutineAlreadyRunning = false;
    }
}
