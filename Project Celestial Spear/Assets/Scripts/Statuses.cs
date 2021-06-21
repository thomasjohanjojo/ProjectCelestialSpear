using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statuses : MonoBehaviour
{
    /// <summary>
    /// This script manages the status values and effects of the gameobject to which this is attached
    /// </summary>


    // The statuses and their respective functions, in that order for each status-functions group
    public int health = 100;

    private GameObject parentGameObject;

    public void DecreaseHealthByTheNumber(int healthToBeDecreased)
    {
        health = health - healthToBeDecreased;
        Debug.Log("damage dealt");

    }
    
    
    // Start is called before the first frame update
    void Start()
    {
         // simply for testing    
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0.0f)
        {
            if (this.transform.parent)
            {
                parentGameObject = this.transform.parent.gameObject;
                Destroy(this.gameObject);
                Destroy(parentGameObject);
            }

            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
