using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoistingScriptOfPlatform : MonoBehaviour
{


    public Transform hoistingPointTransform;
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
        if(collision.tag == "Player" )
        {
            Debug.Log("Player detected under transform");
            collision.gameObject.transform.position = hoistingPointTransform.position; 

        }

        else if(collision.tag == "Enemy")
        {
            Transform enemyTransform;

            enemyTransform = collision.GetComponentInParent<Transform>();
            enemyTransform.position = hoistingPointTransform.position;
            enemyTransform = null;
        }
    }
}
