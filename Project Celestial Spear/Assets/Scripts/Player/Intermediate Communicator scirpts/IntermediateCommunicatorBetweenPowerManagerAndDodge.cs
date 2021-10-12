using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediateCommunicatorBetweenPowerManagerAndDodge : MonoBehaviour
{
    public PowerMangerScript powerManagerScriptReference;
    public PlayerDodge playerDodgeScriptReference;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerDodgeScriptReference.dodgeHasBeenPerformedAnnoucerBoolean == true)
        {
            playerDodgeScriptReference.dodgeHasBeenPerformedAnnoucerBoolean = false;
            powerManagerScriptReference.turnOnThePowerUsagePeriodTimer = true;
        }
        
    }
}
