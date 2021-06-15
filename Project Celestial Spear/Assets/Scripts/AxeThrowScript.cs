using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrowScript : MonoBehaviour
{

    public Transform transformOfThePlayer;

    public Vector3 directionToThePlayer;

    public Transform transformOfThisAxe;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // calculate the direction and normalize it and move towards it.
        directionToThePlayer = transformOfThePlayer.position - transformOfThisAxe.position;
        directionToThePlayer.Normalize();
        transform.Translate(directionToThePlayer * Time.deltaTime);

    }

    
}
