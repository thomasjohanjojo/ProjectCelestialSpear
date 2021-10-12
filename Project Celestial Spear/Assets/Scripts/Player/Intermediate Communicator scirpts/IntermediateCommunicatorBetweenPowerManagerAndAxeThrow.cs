using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediateCommunicatorBetweenPowerManagerAndAxeThrow : MonoBehaviour
{
    public PowerMangerScript powerManagerScriptReference;
    public AxeThrowRaw axeThrowRawScriptReference;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(powerManagerScriptReference.canUsePowers == true)
        {
            axeThrowRawScriptReference.canDetectInputFromPlayerControlBoolean = true;
        }

        else if(powerManagerScriptReference.canUsePowers == false)
        {
            axeThrowRawScriptReference.canDetectInputFromPlayerControlBoolean = false;
        }
    }
}
