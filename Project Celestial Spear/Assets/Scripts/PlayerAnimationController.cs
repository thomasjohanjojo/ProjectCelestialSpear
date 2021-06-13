using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Rigidbody2D myRigidbody2D;

    public Animator myAnimator;

    private string currentState;


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
