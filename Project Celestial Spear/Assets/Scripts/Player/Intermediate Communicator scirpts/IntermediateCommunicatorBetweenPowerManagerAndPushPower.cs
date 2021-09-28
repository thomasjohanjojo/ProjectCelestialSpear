using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediateCommunicatorBetweenPowerManagerAndPushPower : MonoBehaviour
{
    public PowerMangerScript powerManagerScriptReference;
    public PushPowerControlScript pushPowerControlScriptReference;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(powerManagerScriptReference.canUsePowers == true)
        {
            pushPowerControlScriptReference.canDoThePowerAttackControlBoolean = true;
        }

        else if(powerManagerScriptReference.canUsePowers == false)
        {
            pushPowerControlScriptReference.canDoThePowerAttackControlBoolean = false;
        }
        
    }
}
