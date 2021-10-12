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

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            isVictimInProximity = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            isVictimInProximity = false;
        }
    }
}
