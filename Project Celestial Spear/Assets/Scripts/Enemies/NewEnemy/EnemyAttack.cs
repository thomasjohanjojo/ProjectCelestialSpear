using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public AttackRangeDetectionColliderScript attackRangeDetectionColliderScriptReference;
    public AttackGivingColliderScript attackGivingColliderScriptReference;

    public EnemyAnimationCustomController enemyAnimationCustomControllerReference;
    public bool EnemyAttackScriptControlBoolean;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyAttackScriptControlBoolean == true)
        {
            attackThePlayerIfHeIsInAttackingRange();
        }
                
    }

    public void attackThePlayerIfHeIsInAttackingRange()
    {
        if(attackRangeDetectionColliderScriptReference.playerIsInAttackRange)
        {
            //attack the player by playing the attack animation and turning on and off the attack giving collider
        }
    }
    
}
