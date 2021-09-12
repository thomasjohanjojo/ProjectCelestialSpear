using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveResetLocationsScript : MonoBehaviour
{

    public WaveMasterScript waveMasterScriptReference;

    public bool ifThisIsTrueThisLocationWouldStartWavesOtherwiseResetWaves;

    public string tagOfPlayer;
    

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
        if(collision.tag == tagOfPlayer)
        {
            if (ifThisIsTrueThisLocationWouldStartWavesOtherwiseResetWaves == true)
            {
                StartTheWavesIfNotStarted();
            }

            else if(ifThisIsTrueThisLocationWouldStartWavesOtherwiseResetWaves == false)
            {
                StartTheWavesIfNotStarted();

                RestartTheWavesIfFinished();
            }
        }
    }

    private void StartTheWavesIfNotStarted()
    {
        if(waveMasterScriptReference.hasStartedTheWaves == false)
        {
            waveMasterScriptReference.waveMasterScriptControlBoolean = true;
        }
    }

    private void RestartTheWavesIfFinished()
    {
        if(waveMasterScriptReference.hasStartedTheWaves == true && waveMasterScriptReference.hasFinishedAllWaves == true)
        {
            waveMasterScriptReference.ResetAllPropertiesAndRestartTheWaves();
        }
    }
}
