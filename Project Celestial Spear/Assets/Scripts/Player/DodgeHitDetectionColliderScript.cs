using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeHitDetectionColliderScript : MonoBehaviour
{

    public BoxCollider2D boxColliderForDetectingDodgeHits;

    public bool hasAHitBeenDetectedDuringInvincibilityPhase;
    // Start is called before the first frame update
    void Start()
    {
        boxColliderForDetectingDodgeHits.enabled = false;
        hasAHitBeenDetectedDuringInvincibilityPhase = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(boxColliderForDetectingDodgeHits.enabled == false)
        {
            hasAHitBeenDetectedDuringInvincibilityPhase = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "CanDamagePlayer")
        {
            hasAHitBeenDetectedDuringInvincibilityPhase = true;
        }
    }
}
