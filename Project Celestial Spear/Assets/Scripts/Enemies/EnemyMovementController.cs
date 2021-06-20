using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    private Statuses statusSciptOfEnemy;
    private Statuses statusScriptOfPlayer;

    public GameObject player;
    public enum State
    {
        Moving,
        Dead
    }

    private State currentState;

    [SerializeField]
    private float
            groundCheckDistance,
            wallCheckDistance,
            enemyMovementSpeed,
            lastTouchDamageTime,
            touchDamageCooldown,
            touchDamageWidth,
            touchDamageHeight;

    [SerializeField]
    private Transform
        groundCheck,
        touchDamageCheck,
        wallCheck;
    [SerializeField]
    private LayerMask whatIsGround,
                        whatIsPlayer;
    [SerializeField]
    private int
                touchDamage;
    private int facingDirection;

    private Vector2 enemyMovement,
                 touchDamageBotLeft,
                touchDamageTopRight;

    private bool
        groundDetected,
        wallDetected;

    private GameObject alive;
    private Rigidbody2D aliveRb;

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();
        facingDirection = -1;
        statusSciptOfEnemy = aliveRb.gameObject.GetComponent<Statuses>();
        statusScriptOfPlayer = player.GetComponent<Statuses>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }

        if (statusSciptOfEnemy.health <= 0.0f)
        {
           SwitchState(State.Dead);
        }
    }

    // MOVING STATE
    
    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        CheckTouchDamage();

        if (!groundDetected || wallDetected)
        {
            //Flip
            Flip();
        }
        else 
        {
            //Move
            enemyMovement.Set(enemyMovementSpeed * facingDirection, aliveRb.velocity.y);
            aliveRb.velocity = enemyMovement;
        }
    }

    private void ExitMovingState()
    {

    }

    // DEAD STATE

    private void EnterDeadState()
    {
        Debug.Log("Entered dead state");
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    // Other Functions

    private void CheckTouchDamage()
    {
        if (Time.time >= lastTouchDamageTime + touchDamageCooldown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

            if (hit != null)
            {
                lastTouchDamageTime = Time.time;
                statusScriptOfPlayer.DecreaseHealthByTheNumber(touchDamage);
            }
        }
    }



    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);

    }

    private void SwitchState(State state)
    {
        switch(currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);
    }
}
