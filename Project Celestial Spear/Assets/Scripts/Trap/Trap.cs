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

    public int damageToBeGivenToVictim;

    public bool victimIsInDamageArea;

    public BoxCollider2D damageGivingBoxColliderOfTheTrap;

    public bool theTrapCoroutineIsPlaying;

    public Statuses statusScriptOfThePlayer;

    public EnemyStatusScript statusScriptOfTheEnemy;       

    public GameObject gameObjectOfTheVictim;

    // Start is called before the first frame update
    void Start()
    {
        victimIsInDamageArea = false;
        theTrapCoroutineIsPlaying = false;
        damageGivingBoxColliderOfTheTrap.enabled = false;
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
            gameObjectOfTheVictim = collision.gameObject;

            if (gameObjectOfTheVictim.GetComponent<Statuses>())
            {
                statusScriptOfThePlayer = gameObjectOfTheVictim.GetComponent<Statuses>();
            }

            victimIsInDamageArea = true;
        }


        if (collision.tag == "Enemy")
        {
            gameObjectOfTheVictim = collision.gameObject;

            if (gameObjectOfTheVictim.GetComponentInChildren<EnemyStatusScript>())
            {
                statusScriptOfTheEnemy = gameObjectOfTheVictim.GetComponentInChildren<EnemyStatusScript>();
            }

            victimIsInDamageArea = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            gameObjectOfTheVictim = null;
            statusScriptOfThePlayer = null;
            statusScriptOfTheEnemy = null;
            victimIsInDamageArea = false;
        }
    }

    public IEnumerator PlayTheTrapAnimationsAndDealDamageIfPossible()
    {
        theTrapCoroutineIsPlaying = true;

        trapAnimationControllerScriptReference.ChangeAnimationState(trapAnimationControllerScriptReference.TRAP_COMING_OUT_ANIMATION);
        yield return new WaitForSeconds(durationOfTheComingOutAnimation);
        trapAnimationControllerScriptReference.ChangeAnimationState(trapAnimationControllerScriptReference.TRAP_IS_AT_THE_TOP_MOST);

        damageGivingBoxColliderOfTheTrap.enabled = true;

        if (victimIsInDamageArea)
        {
            if(gameObjectOfTheVictim)
            {
                if(statusScriptOfThePlayer)
                {
                    statusScriptOfThePlayer.DecreaseHealthByTheNumber(damageToBeGivenToVictim, damageGivingBoxColliderOfTheTrap);
                }

                if(statusScriptOfTheEnemy)
                {
                    statusScriptOfTheEnemy.DecreaseHealthByTheNumber(damageToBeGivenToVictim);
                }

            }
        }

        yield return new WaitForSeconds(durationOfTheWaitTimeBetweenAnimation);

        damageGivingBoxColliderOfTheTrap.enabled = false;
        trapAnimationControllerScriptReference.ChangeAnimationState(trapAnimationControllerScriptReference.TRAP_COMING_IN_ANIMATION);
        yield return new WaitForSeconds(durationOfTheGoingInAnimation);
        
        trapAnimationControllerScriptReference.ChangeAnimationState(trapAnimationControllerScriptReference.TRAP_IDLE_ANIMATION);
        yield return new WaitForSeconds(durationOfTheWaitTimeBetweenAnimation);

        theTrapCoroutineIsPlaying = false;
    }

}
