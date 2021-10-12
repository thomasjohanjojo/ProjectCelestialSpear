using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeEnemyFlashRedWhenAttacked : MonoBehaviour
{
    public EnemyStatusScript enemyStatusScriptReference;

    public float millisecondsForStayingRedDuringFlashing;

    public SpriteRenderer spriteRendererReference;

    public Color colorWhenFlashingRed;
    public Color normalColor;

    private bool oneInstanceOfTheCoroutineIsAlreadyRunning;

    // Start is called before the first frame update
    void Start()
    {
        oneInstanceOfTheCoroutineIsAlreadyRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        MakeEnemyFlashRedWheneverHasBeenAttackedBooleanOfEnemyStatusScriptIsTrue();
    }

    private void MakeEnemyFlashRedWheneverHasBeenAttackedBooleanOfEnemyStatusScriptIsTrue()
    {
        if(enemyStatusScriptReference.hasBeenAttacked == true)
        {
            enemyStatusScriptReference.hasBeenAttacked = false;

            if (oneInstanceOfTheCoroutineIsAlreadyRunning == false)
            {
                StartCoroutine(MakeEnemyFlashRed());
            }

            else if (oneInstanceOfTheCoroutineIsAlreadyRunning == true)
            {
                StopCoroutine(MakeEnemyFlashRed());
                oneInstanceOfTheCoroutineIsAlreadyRunning = false;
                ReturnColorBackToNormalColor();
                StartCoroutine(MakeEnemyFlashRed());
            }
        }
    }

    private IEnumerator MakeEnemyFlashRed()
    {
        oneInstanceOfTheCoroutineIsAlreadyRunning = true;
        spriteRendererReference.color = colorWhenFlashingRed;
        yield return new WaitForSeconds(millisecondsForStayingRedDuringFlashing);
        spriteRendererReference.color = normalColor;
        oneInstanceOfTheCoroutineIsAlreadyRunning = false;
    }

    private void ReturnColorBackToNormalColor()
    {
        spriteRendererReference.color = normalColor;
    }
}
