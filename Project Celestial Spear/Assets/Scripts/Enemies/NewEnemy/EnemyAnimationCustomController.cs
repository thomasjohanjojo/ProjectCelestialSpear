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
