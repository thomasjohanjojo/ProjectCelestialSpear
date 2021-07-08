using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationCustomController : MonoBehaviour
{
 

    public Animator myAnimator;

    private string currentState;

    public string ENEMY_WALK_ANIMATION = "Enemy_walk";
    public string ENEMY_IDLE_ANIMATION = "Enemy_idle";
    public string ENEMY_ATTACK_ANIMATION = "Enemy_attack";

    public float ENEMY_ATTACK_ANIMATION_DURATION;

    private bool theAnimationHasStartedAndTheStartingTimeHasBeenNoted;
    private float startingTimeOfTheAnimation;

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


    public bool CheckIfAnimationHasCompleted(float duration)
    {
        if(theAnimationHasStartedAndTheStartingTimeHasBeenNoted == false)
        {
            startingTimeOfTheAnimation = Time.time;
            theAnimationHasStartedAndTheStartingTimeHasBeenNoted = true;
            return false;
        }

        if(theAnimationHasStartedAndTheStartingTimeHasBeenNoted == true)
        {
            if(Time.time - startingTimeOfTheAnimation > duration)
            {
                theAnimationHasStartedAndTheStartingTimeHasBeenNoted = false;
                return true;
            }

            else if(Time.time - startingTimeOfTheAnimation < duration)
            {
                return false;
            }

            else
            {
                return false;
            }
        }

        else
        {
            return false;
        }
    }
}
