using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediateCommunicatorBetweenFacingDirectionDeterminerAndAxeThrow : MonoBehaviour
{

    public FacingDirectionDeterminationScript facingDirectionDeterminationScriptReference;
    public AxeThrowRaw axeThrowScriptReference;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        axeThrowScriptReference.playerFacingDirectionToBeObtainedFromSomeOtherScript = facingDirectionDeterminationScriptReference.facingDirection;
    }
}
