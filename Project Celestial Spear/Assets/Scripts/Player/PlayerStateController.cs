using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{

    public AxeThrowScript axeThrowScriptReference;
    public PlayerAttack playerAttackScriptReference;
    public MainCharacterBasicMovement mainCharacterBasicMovementScriptReference;
    public PlayerDodge playerDodgeScriptReference;

    public string PLAYER_STATE_IDLE_OR_MOVING = "PlayerStateIdleOrMoving";
    public string PLAYER_STATE_ATTACKING = "PlayerStateAttacking";
    public string PLAYER_STATE_AXE_THROW = "PlayerStateAxeThrow";
    public string PLAYER_STATE_DODGING = "PlayerStateDodging";

    
    

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }


    public void DisableEverythingElseForDoingDodge()
    {
        axeThrowScriptReference.axeThrowScriptOnOffBoolean = false;
        mainCharacterBasicMovementScriptReference.mainCharacterBasicMovementScriptOnOffBoolean = false;
        playerAttackScriptReference.PlayerAttackScriptOnOffBoolean = false;
    }

    public void EnableEverythingBackAfterDodging()
    {
        axeThrowScriptReference.axeThrowScriptOnOffBoolean = true;
        mainCharacterBasicMovementScriptReference.mainCharacterBasicMovementScriptOnOffBoolean = true;
        playerAttackScriptReference.PlayerAttackScriptOnOffBoolean = true;
    }
}
