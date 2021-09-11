using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMasterScript : MonoBehaviour
{
    public GameObject[] listOfWaves;

    private WaveScript waveScriptOfTheCurrentWave;

    public int currentWaveNumber;

    public bool HasStartedTheWaves;
    public bool HasFinishedAllWaves;
    // Start is called before the first frame update
    void Start()
    {
        currentWaveNumber = 0;
        HasStartedTheWaves = false;
        HasFinishedAllWaves = false;
    }

    // Update is called once per frame
    void Update()
    {
        WaveMasterScriptMainFunction();
    }

    private void WaveMasterScriptMainFunction()
    {
        if(HasStartedTheWaves == false)
        {
            StartTheFirstWave();
        }

        else if(HasStartedTheWaves == true && HasFinishedAllWaves == false)
        {
            LoadTheNextWaveIfTheCurrentWaveHasFinished();
        }

        
    }

    private void StartTheFirstWave()
    {
        currentWaveNumber = 0;

        waveScriptOfTheCurrentWave = listOfWaves[currentWaveNumber].GetComponent<WaveScript>();

        waveScriptOfTheCurrentWave.WaveScriptControlBoolean = true;

        HasStartedTheWaves = true;
    }

    private void LoadTheNextWaveIfTheCurrentWaveHasFinished()
    {
        if(waveScriptOfTheCurrentWave.HasWaveFinishedProgressionBoolean == true)
        {
            currentWaveNumber++;

            if(currentWaveNumber >= listOfWaves.Length)
            {
                HasFinishedAllWaves = true;
            }

            else
            {
                waveScriptOfTheCurrentWave = listOfWaves[currentWaveNumber].GetComponent<WaveScript>();

                waveScriptOfTheCurrentWave.WaveScriptControlBoolean = true;
            }
        }
    }

}
