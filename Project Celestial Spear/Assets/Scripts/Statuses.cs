using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statuses : MonoBehaviour
{
    /// <summary>
    /// This script manages the status values and effects of the gameobject to which this is attached
    /// </summary>


    // The statuses and their respective functions
    public int health = 100;

    public void DecreaseHealthByTheNumber(int healthToBeDecreased)
    {
        health = health - healthToBeDecreased;
    }

    // Start is called before the first frame update
    void Start()
    {
        DecreaseHealthByTheNumber(10);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
