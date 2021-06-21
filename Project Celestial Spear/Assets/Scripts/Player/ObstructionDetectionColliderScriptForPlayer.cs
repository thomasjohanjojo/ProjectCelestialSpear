using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionDetectionColliderScriptForPlayer : MonoBehaviour
{

    public bool ObstructionInFront;
    public float detectionDistanceOfRaycast;
    
    public PlayerAttack playerAttackScriptReference;

    public LayerMask layerMaskOfEnemy;


    // Start is called before the first frame update
    void Start()
    {
        ObstructionInFront = false;
    }

    // Update is called once per frame
    void Update()
    {

        playerAttackScriptReference.CheckPlayerFacingDirection();

        
       if(IfEnemyInRange() == true)
       {
           ObstructionInFront = true;
       }

       else if(IfEnemyInRange() == false)
       {
           ObstructionInFront = false;
       }

    }

    private bool IfEnemyInRange()
    {
        Vector2 directionOfRaycast = new Vector2(playerAttackScriptReference.playerFacingDirection, 0);

        return Physics2D.Raycast(transform.position, directionOfRaycast, detectionDistanceOfRaycast, layerMaskOfEnemy);
    }


}
