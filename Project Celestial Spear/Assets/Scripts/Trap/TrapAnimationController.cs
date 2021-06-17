using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapAnimationController : MonoBehaviour
{
    public Animator animatorOfTheTrap;

    

    string currentState;


    string TRAP_IDLE_ANIMATION = "TrapIdle";
    string TRAP_COMING_OUT_ANIMATION = "TrapcomingOut";
    string TRAP_COMING_IN_ANIMATION = "TrapComingIn";

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
        if (newState == currentState)
        {
            return;
        }

        else if (newState != currentState)
        {
            animatorOfTheTrap.Play(newState);
            currentState = newState;
        }
    }
}
