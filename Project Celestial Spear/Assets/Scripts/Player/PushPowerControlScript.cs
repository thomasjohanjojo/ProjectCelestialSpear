using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPowerControlScript : MonoBehaviour
{

    public PlayerAttack playerAttackScriptReference;

    public GameObject pushPowerUIObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            playerAttackScriptReference.CanPush = !playerAttackScriptReference.CanPush;

            if (playerAttackScriptReference.CanPush == true)
            {
                pushPowerUIObject.SetActive(true);
            }

            else if (playerAttackScriptReference.CanPush == false)
            {
                pushPowerUIObject.SetActive(false);
            }
        }

        
    }
}
