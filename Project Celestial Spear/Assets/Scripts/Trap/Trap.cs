using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    public TrapAnimationController trapAnimationControllerScriptReference;

    public ProximityDetectionColliderScript proximityDetectionColliderScriptReference;

    public float durationOfTheComingOutAnimation;
    public float durationOfTheWaitTimeBetweenAnimation;
    public float durationOfTheGoingInAnimation;

    public int damageToBeGivenToPlayer;

    public bool victimIsInDamageArea;

    public bool theTrapCoroutineIsPlaying;

    public Statuses statusScriptOfThePlayer;

    public Statuses statusScriptOfTheEnemy;

    public GameObject gameObjectOfThePlayer;

    // Start is called before the first frame update
    void Start()
    {
        victimIsInDamageArea = false;
        theTrapCoroutineIsPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        IfVictimInProximityActiviateTheCoRoutine();
            

       
    }

    public void IfVictimInProximityActiviateTheCoRoutine()
    {
        if(proximityDetectionColliderScriptReference.isVictimInProximity == true && theTrapCoroutineIsPlaying == false)
        {
            StartCoroutine(PlayTheTrapAnimationsAndDealDamageIfPossible());
        }

        else if(proximityDetectionColliderScriptReference.isVictimInProximity == false)
        {
            StopCoroutine(PlayTheTrapAnimationsAndDealDamageIfPossible());
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gameObjectOfThePlayer = collision.gameObject;

            if (gameObjectOfThePlayer.GetComponent<Statuses>())
            {
                statusScriptOfThePlayer = gameObjectOfThePlayer.GetComponent<Statuses>();
            }

            victimIsInDamageArea = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gameObjectOfThePlayer = null;
            statusScriptOfThePlayer = null;
            victimIsInDamageArea = false;
        }
    }

    public IEnumerator PlayTheTrapAnimationsAndDealDamageIfPossible()
    {
        theTrapCoroutineIsPlaying = true;

        trapAnimationControllerScriptReference.ChangeAnimationState(trapAnimationControllerScriptReference.TRAP_COMING_OUT_ANIMATION);
        yield return new WaitForSeconds(durationOfTheComingOutAnimation);
        trapAnimationControllerScriptReference.ChangeAnimationState(trapAnimationControllerScriptReference.TRAP_IS_AT_THE_TOP_MOST);

        if (victimIsInDamageArea)
        {
            if(gameObjectOfThePlayer)
            {
                if(statusScriptOfThePlayer)
                {
                    statusScriptOfThePlayer.DecreaseHealthByTheNumber(damageToBeGivenToPlayer);
                }
            }
        }

        yield return new WaitForSeconds(durationOfTheWaitTimeBetweenAnimation);

        trapAnimationControllerScriptReference.ChangeAnimationState(trapAnimationControllerScriptReference.TRAP_COMING_IN_ANIMATION);
        yield return new WaitForSeconds(durationOfTheGoingInAnimation);
        
        trapAnimationControllerScriptReference.ChangeAnimationState(trapAnimationControllerScriptReference.TRAP_IDLE_ANIMATION);
        yield return new WaitForSeconds(durationOfTheWaitTimeBetweenAnimation);

        theTrapCoroutineIsPlaying = false;
    }

}
