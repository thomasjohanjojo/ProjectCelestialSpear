using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelSectionsControllerScript : MonoBehaviour
{


    

    public IntermediateScriptforLerpingTheCameraWhenNecessary IntermediateScriptforLerpingTheCameraWhenNecessaryScriptReference;

    public string nameOfThisSection;

    public bool isActive;

    public Transform theTransformOfThePositionWhereTheCameraShouldLookAt;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {


        if(collision.tag == "Player")
        {
            IntermediateScriptforLerpingTheCameraWhenNecessaryScriptReference.ChangeActiveSection(nameOfThisSection, theTransformOfThePositionWhereTheCameraShouldLookAt.position);
            isActive = true;
        }

        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isActive = false;
        }
    }



}
