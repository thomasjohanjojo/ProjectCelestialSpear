using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPowerControlScript : MonoBehaviour
{

    public PlayerAttack playerAttackScriptReference;

    public bool canDoThePowerAttackControlBoolean;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canDoThePowerAttackControlBoolean)
        {
            playerAttackScriptReference.canPush = true;
                       
        }

        
    }
}
