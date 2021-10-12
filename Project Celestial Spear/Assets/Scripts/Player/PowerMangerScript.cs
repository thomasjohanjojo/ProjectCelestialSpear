using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerMangerScript : MonoBehaviour
{

    public bool canUsePowers;

    public float timePeriodDuringWhichPowersCanUsed;

    public bool turnOnThePowerUsagePeriodTimer;

    private bool oneCoroutineIsAlreadyRunningNow;

    

    // Start is called before the first frame update
    void Start()
    {
        canUsePowers = false;
        turnOnThePowerUsagePeriodTimer = false;
        oneCoroutineIsAlreadyRunningNow = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(turnOnThePowerUsagePeriodTimer == true && oneCoroutineIsAlreadyRunningNow == false)
        {
            StartCoroutine(CanUsePowersDuringThisTimePeriod());
        }
    }

    private IEnumerator CanUsePowersDuringThisTimePeriod()
    {
        turnOnThePowerUsagePeriodTimer = false;

        oneCoroutineIsAlreadyRunningNow = true;

        canUsePowers = true;

        yield return new WaitForSeconds(timePeriodDuringWhichPowersCanUsed);

        canUsePowers = false;

        oneCoroutineIsAlreadyRunningNow = false;
    }
}
