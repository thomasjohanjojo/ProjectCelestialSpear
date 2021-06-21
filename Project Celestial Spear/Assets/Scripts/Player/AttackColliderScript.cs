using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderScript : MonoBehaviour
{
    public Statuses statusSciptOfEnemy;
    public Rigidbody2D enemyRigidBody;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerStay2D(Collider2D collision) // This function is automatically called by unity like the update function
    {
        if (collision.tag == "Enemy")
        {


            enemyRigidBody = collision.gameObject.GetComponentInChildren<Rigidbody2D>();

            if (enemyRigidBody)
            {
                statusSciptOfEnemy = collision.gameObject.GetComponentInChildren<Statuses>();
            }

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            enemyRigidBody = null;
            statusSciptOfEnemy = null;
        }
    }
}