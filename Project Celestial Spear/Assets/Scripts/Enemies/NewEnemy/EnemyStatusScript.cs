using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusScript : MonoBehaviour
{
    public int health = 100;

    public bool hasBeenAttacked;

    private GameObject parentGameObject;

    public bool healthHasReachedZero;

    public void DecreaseHealthByTheNumber(int healthToBeDecreased)
    {
        hasBeenAttacked = true;
        if (health != 0)
        {
            health = health - healthToBeDecreased;
        }
        
        if(health <= 0)
        {
            healthHasReachedZero = true;
        }


    }


    // Start is called before the first frame update
    void Start()
    {
        hasBeenAttacked = false;
        healthHasReachedZero = false;
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
