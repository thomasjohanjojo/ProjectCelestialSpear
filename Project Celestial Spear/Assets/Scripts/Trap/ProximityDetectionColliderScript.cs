using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityDetectionColliderScript : MonoBehaviour
{

    public bool isVictimInProximity;
    // Start is called before the first frame update
    void Start()
    {
        isVictimInProximity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isVictimInProximity = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isVictimInProximity = false;
        }
    }
}
