using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrowAnimatorScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator myAnimator;

    private string currentState;



    public string IDLE = "AxeInvisible";
    public string VERTICAL   = "RangedVertical";
    public string HORIZONTAL = "RangedHorizontal";
    


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
