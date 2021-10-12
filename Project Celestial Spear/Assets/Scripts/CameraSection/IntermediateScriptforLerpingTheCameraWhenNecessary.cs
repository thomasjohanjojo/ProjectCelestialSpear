using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntermediateScriptforLerpingTheCameraWhenNecessary : MonoBehaviour
{

    public CinemachineVirtualCamera virtualCameraOfThecinemachine;

    

    private string nameOfCurrentActiveSection;
    private string nameOfNewSection;
    private Vector3 positionOfTheCurrentSection;
    private Vector3 positionOfTheCurrentCameraFollow;
    private Vector3 positionOfTheNewSection;

    public Transform transformToBeUpdatedToTheFollow;

    private bool doLerpingBetweenActiveSections;

    private bool onOffBooleanForCheckingIfTargetCameraPositionHasReachedFunction;

    private float timeWhenLerpingStarted;
    private float timeSinceLerpingStarted;

    public float lerpingDurationBetweenActiveSections;



    // Start is called before the first frame update
    void Start()
    {
        doLerpingBetweenActiveSections = false;
        onOffBooleanForCheckingIfTargetCameraPositionHasReachedFunction = false;
        virtualCameraOfThecinemachine.Follow = transformToBeUpdatedToTheFollow;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTheValueOfTheCameraFollowTransformPosition();

        LerpBetweenTheActiveSectionCameraLocations();
        CheckIfTargetCameraPositionHasReached();
        
    }


    private void UpdateTheValueOfTheCameraFollowTransformPosition()
    {
        transformToBeUpdatedToTheFollow.position = positionOfTheCurrentCameraFollow;
        virtualCameraOfThecinemachine.Follow = transformToBeUpdatedToTheFollow;
    }


    public void ChangeActiveSection(string nameOfNewSection, Vector3 positionOfTheNewSection)
    {
        if(nameOfNewSection != nameOfCurrentActiveSection)
        {
            this.nameOfNewSection = nameOfNewSection;
            this.positionOfTheNewSection = positionOfTheNewSection;
            timeWhenLerpingStarted = Time.time;
            doLerpingBetweenActiveSections = true;
            onOffBooleanForCheckingIfTargetCameraPositionHasReachedFunction = true;
        }
    }

    private void CheckIfTargetCameraPositionHasReached()
    {
        if(onOffBooleanForCheckingIfTargetCameraPositionHasReachedFunction == true)
        {
            // check if it has and then turn off the onOffBoolean
            // turnOf doLerpingBetweenActiveSections
            // nameofcurrentactivesection = nameofnewsection

            if(positionOfTheCurrentCameraFollow == positionOfTheNewSection)
            {
                nameOfCurrentActiveSection = nameOfNewSection;
                positionOfTheCurrentSection = positionOfTheNewSection;

                onOffBooleanForCheckingIfTargetCameraPositionHasReachedFunction = false;
                doLerpingBetweenActiveSections = false;
            }


        }
       
        
    }

    private void LerpBetweenTheActiveSectionCameraLocations()
    {
        if(doLerpingBetweenActiveSections == true)
        {
            timeSinceLerpingStarted = Time.time - timeWhenLerpingStarted;

            float percentageComplete = timeSinceLerpingStarted / lerpingDurationBetweenActiveSections;

            positionOfTheCurrentCameraFollow = Vector3.Lerp(positionOfTheCurrentSection, positionOfTheNewSection, percentageComplete);
        }
    }
}
