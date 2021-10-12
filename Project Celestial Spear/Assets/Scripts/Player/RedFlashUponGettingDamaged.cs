using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFlashUponGettingDamaged : MonoBehaviour
{
    public Statuses PlayerStatusScriptReference;

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
        MakePlayerFlashRedWheneverHasBeenDamagedBooleanOfEnemyStatusScriptIsTrue();
    }

    private void MakePlayerFlashRedWheneverHasBeenDamagedBooleanOfEnemyStatusScriptIsTrue()
    {
        if (PlayerStatusScriptReference.playerHasBeenDamaged == true)
        {
            PlayerStatusScriptReference.playerHasBeenDamaged = false;

            if (oneInstanceOfTheCoroutineIsAlreadyRunning == false)
            {
                StartCoroutine(MakePlayerFlashRed());
            }

            else if (oneInstanceOfTheCoroutineIsAlreadyRunning == true)
            {
                StopCoroutine(MakePlayerFlashRed());
                oneInstanceOfTheCoroutineIsAlreadyRunning = false;
                ReturnColorBackToNormalColor();
                StartCoroutine(MakePlayerFlashRed());
            }
        }
    }

    private IEnumerator MakePlayerFlashRed()
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
