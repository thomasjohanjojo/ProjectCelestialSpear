using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour
{
    public Transform[] listOfSpawningLocations;

    private int numberOfEnemyObjectsCurrectlyAlive = 0;

    public GameObject MidSizeEnemy;
    private InstantiateUIElementUponStart instantiateUIElementsUponStartScriptTemporaryReference;

    public Canvas canvasReference;
    public Camera MainCamera;

    private GameObject enemyGameObject;
    private EnemyWaveManagementCompanionScript enemyWaveManagerCompanionScriptReference;
    

    private GameObject enemyHealthTemporaryObject;
    private EnemyHealthUIDisplayScript enemyHealthUIDisplayScriptTemporaryReference;

    public bool WaveScriptControlBoolean;

    public bool HasFinishedInstantiatingEnemiesProgressionBoolean;
    public bool HasAllEnemiesBeenKilledProgressionBoolean;
    public bool HasWaveFinishedProgressionBoolean;

    // Start is called before the first frame update
    void Start()
    {
        WaveScriptControlBoolean = false;
        HasFinishedInstantiatingEnemiesProgressionBoolean = false;
        HasAllEnemiesBeenKilledProgressionBoolean = false;
        HasWaveFinishedProgressionBoolean = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (WaveScriptControlBoolean == true)
        {
            MainFunctionAccordingToTheProgresionOnTheBasisOfProgressionBooleans();
        }
    }

    private void MainFunctionAccordingToTheProgresionOnTheBasisOfProgressionBooleans()
    {
        if (HasFinishedInstantiatingEnemiesProgressionBoolean == false)
        {
            InstantiateEnemiesAtLocations();
            HasFinishedInstantiatingEnemiesProgressionBoolean = true;
        }

        else if(HasFinishedInstantiatingEnemiesProgressionBoolean == true && HasAllEnemiesBeenKilledProgressionBoolean == false)
        {
            if(numberOfEnemyObjectsCurrectlyAlive == 0)
            {
                HasAllEnemiesBeenKilledProgressionBoolean = true;
                HasWaveFinishedProgressionBoolean = true;
            }
        }

        

    }

    private void InstantiateEnemiesAtLocations()
    {
         

        foreach(Transform location in listOfSpawningLocations)
        {
            
            enemyGameObject =  Instantiate(MidSizeEnemy, location.position, MidSizeEnemy.transform.rotation);

            AssignTheReferenceToThisWaveScriptWhichHadCreatedTheEnemyGameObject();
            AssignTheUIRefernces();
            
            numberOfEnemyObjectsCurrectlyAlive++;
        }
    }


    private void AssignTheUIRefernces()
    {
        instantiateUIElementsUponStartScriptTemporaryReference = enemyGameObject.GetComponent<InstantiateUIElementUponStart>();
        instantiateUIElementsUponStartScriptTemporaryReference.mainCamera = MainCamera;
        instantiateUIElementsUponStartScriptTemporaryReference.canvasReference = canvasReference;
    }

    private void AssignTheReferenceToThisWaveScriptWhichHadCreatedTheEnemyGameObject()
    {
        enemyWaveManagerCompanionScriptReference = enemyGameObject.GetComponent<EnemyWaveManagementCompanionScript>();
        enemyWaveManagerCompanionScriptReference.waveSriptWhichCreatedThisEnemyReference = this;
    }

    

    public void EnemyHasBeenKilledSoSubtractFromTheAliveEnemyNumber()
    {
        numberOfEnemyObjectsCurrectlyAlive--;
    }
}
