using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMasterScript : MonoBehaviour
{
    public GameObject[] listOfWaves;

    private WaveScript waveScriptOfTheCurrentWave;

    public int currentWaveNumber;

    public bool waveMasterScriptControlBoolean;

    public bool hasStartedTheWaves;
    public bool hasFinishedAllWaves;
    // Start is called before the first frame update
    void Start()
    {
        currentWaveNumber = 0;
        hasStartedTheWaves = false;
        hasFinishedAllWaves = false;
        waveMasterScriptControlBoolean = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (waveMasterScriptControlBoolean == true)
        {
            WaveMasterScriptMainFunction();
        }
    }

    private void WaveMasterScriptMainFunction()
    {
        if(hasStartedTheWaves == false)
        {
            StartTheFirstWave();
        }

        else if(hasStartedTheWaves == true && hasFinishedAllWaves == false)
        {
            LoadTheNextWaveIfTheCurrentWaveHasFinished();
        }

        
    }

    private void StartTheFirstWave()
    {
        currentWaveNumber = 0;

        waveScriptOfTheCurrentWave = listOfWaves[currentWaveNumber].GetComponent<WaveScript>();

        waveScriptOfTheCurrentWave.WaveScriptControlBoolean = true;

        hasStartedTheWaves = true;
    }

    private void LoadTheNextWaveIfTheCurrentWaveHasFinished()
    {
        if(waveScriptOfTheCurrentWave.HasWaveFinishedProgressionBoolean == true)
        {
            currentWaveNumber++;

            if(currentWaveNumber >= listOfWaves.Length)
            {
                hasFinishedAllWaves = true;
                waveMasterScriptControlBoolean = false;
            }

            else
            {
                

                waveScriptOfTheCurrentWave = listOfWaves[currentWaveNumber].GetComponent<WaveScript>();

                waveScriptOfTheCurrentWave.WaveScriptControlBoolean = true;
            }
        }
    }


    public void ResetAllPropertiesAndRestartTheWaves()
    {
        ResetAllWaves();

        ResetAllMasterScriptProperties();

        waveMasterScriptControlBoolean = true;

    }

    private void ResetAllMasterScriptProperties()
    {
        currentWaveNumber = 0;
        hasStartedTheWaves = false;
        hasFinishedAllWaves = false;
    }

    private void ResetAllWaves()
    {
        WaveScript wave;
        foreach(GameObject waveObject in listOfWaves)
        {
            wave = waveObject.GetComponent<WaveScript>();
            wave.ResetAllPropertiesOfTheWave();
        }
    }

}
