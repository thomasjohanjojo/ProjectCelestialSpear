using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGivingColliderScript : MonoBehaviour
{

    
    public BoxCollider2D damageGivingBoxCollider;

    public GameObject gameObjectOfThePlayer;
    public Statuses statusScriptOfThePlayer;

    public bool damageHasBeenGivenToThePlayerNowTurnTheColliderOff;

    // Start is called before the first frame update
    void Start()
    {
        damageGivingBoxCollider.enabled = false;
        damageHasBeenGivenToThePlayerNowTurnTheColliderOff = false;
    }

    // Update is called once per frame
    void Update()
    {
        IfDamageHasBeenGivenToThePlayerThenTurnOffTheCollider();
    }

    public void IfDamageHasBeenGivenToThePlayerThenTurnOffTheCollider()
    {
        if(damageHasBeenGivenToThePlayerNowTurnTheColliderOff == true)
        {
            damageHasBeenGivenToThePlayerNowTurnTheColliderOff = false;
            damageGivingBoxCollider.enabled = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.gameObject)
            {
                gameObjectOfThePlayer = collision.gameObject;

                if(gameObjectOfThePlayer.GetComponent<Statuses>())
                {
                    statusScriptOfThePlayer = gameObjectOfThePlayer.GetComponent<Statuses>();

                                       
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            statusScriptOfThePlayer = null;
            gameObjectOfThePlayer = null;
            damageGivingBoxCollider.enabled = false;
        }
    }
}
