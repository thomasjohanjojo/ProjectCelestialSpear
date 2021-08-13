using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusScript : MonoBehaviour
{
    public int health = 100;

    public bool hasBeenAttacked;

    private GameObject parentGameObject;

    public void DecreaseHealthByTheNumber(int healthToBeDecreased)
    {
        hasBeenAttacked = true;
        health = health - healthToBeDecreased;
                     

    }


    // Start is called before the first frame update
    void Start()
    {
        hasBeenAttacked = false;
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
