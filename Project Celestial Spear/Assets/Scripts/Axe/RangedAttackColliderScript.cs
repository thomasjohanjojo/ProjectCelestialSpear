using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackColliderScript : MonoBehaviour
{

    public PlayerAttack playerAttackReferenceForGettingHitCounter;

    public int damageOfTheAttack;
    private Rigidbody2D enemyRigidBody;
    private Statuses statusScriptOfTheEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.tag == "Enemy")
        {
            enemyRigidBody = collision.GetComponentInChildren<Rigidbody2D>();
            statusScriptOfTheEnemy = collision.GetComponentInChildren<Statuses>();
            statusScriptOfTheEnemy.DecreaseHealthByTheNumber(damageOfTheAttack * playerAttackReferenceForGettingHitCounter.HitCounterInt);
            statusScriptOfTheEnemy = null;
            enemyRigidBody = null;
        }
    }



}
