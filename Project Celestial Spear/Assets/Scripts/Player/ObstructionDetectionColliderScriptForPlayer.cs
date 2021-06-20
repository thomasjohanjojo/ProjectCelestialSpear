using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionDetectionColliderScriptForPlayer : MonoBehaviour
{

    public bool ObstructionInFront;


    // Start is called before the first frame update
    void Start()
    {
        ObstructionInFront = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            ObstructionInFront = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            ObstructionInFront = false;
        }
    }
}
