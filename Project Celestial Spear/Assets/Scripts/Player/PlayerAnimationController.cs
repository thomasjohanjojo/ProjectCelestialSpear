using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    

    public Animator myAnimator;

    private string currentState;

    

    public  string IDLE_ANIMATION = "Idle";
    public  string RUNNING_ANIMATION = "Running";
    public string PUNCH_AND_PUSH_ANIMATION = "PunchAndPush";
    public string PLAYER_ATTACK_ONE_ANIMATION = "PlayerAttackOne";
    public string PLAYER_ATTACK_TWO_ANIMATION = "PlayerAttackTwo";
    public string AXE_REAPPEAR_TO_HAND_ANIMATION = "AxeReappearToHandAnimation";
    public string HOLD_THE_DODGE_ANIMATION = "HoldTheDodge";
    public string INVINCIBLE_STAGE_ANIMATION = "InvincibleStage";
    public string THE_ACTUAL_DODGE_ANIMATION = "TheActualDodge";
    public string PLAYER_ATTACK_FINAL_ATTACK = "FinalAttack";
    


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        


    }

    



    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
        {
            return;
        }
        else
        {
            myAnimator.Play(newState);

            currentState = newState;
        }
    }
}
