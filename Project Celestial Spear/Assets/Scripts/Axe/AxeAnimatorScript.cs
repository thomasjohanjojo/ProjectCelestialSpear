using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAnimatorScript : MonoBehaviour
{
    public Animator animatorOfTheAxe;

    public AxeThrowScript axeThrowScriptReference;

    string currentState;


    string AXE_THROW_ANIMATION = "AxeThrowAnimation";
    string AXE_INVISIBLE = "AxeInvisible";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(axeThrowScriptReference.isAxeThowing == true)
        {
            ChangeAnimationState(AXE_THROW_ANIMATION);
        }
        
        else if(axeThrowScriptReference.isAxeThowing == false)
        {
            ChangeAnimationState(AXE_INVISIBLE);
        }
    }


    public void ChangeAnimationState(string newState)
    {
        if(newState == currentState)
        {
            return;
        }

        else if(newState != currentState)
        {
            animatorOfTheAxe.Play(newState);
            currentState = newState;
        }
    }
}
