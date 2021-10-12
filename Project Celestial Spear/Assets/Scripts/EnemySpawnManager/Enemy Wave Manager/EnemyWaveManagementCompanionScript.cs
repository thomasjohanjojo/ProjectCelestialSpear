using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManagementCompanionScript : MonoBehaviour
{

    public EnemyStatusScript enemyStatusScriptReference;

    public GameObject gameObjectOfThisEnemy;

    public WaveScript waveSriptWhichCreatedThisEnemyReference;

    public InstantiateUIElementUponStart instantiateUIElementUponStartScriptReference;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyStatusScriptReference.healthHasReachedZero == true)
        {
            DestroyThisEnemyAndTellTheWaveScriptThatThisHasDied();
        }
    }

    

    private void DestroyThisEnemyAndTellTheWaveScriptThatThisHasDied()
    {
        waveSriptWhichCreatedThisEnemyReference.EnemyHasBeenKilledSoSubtractFromTheAliveEnemyNumber();
        Destroy(instantiateUIElementUponStartScriptReference.enemyHealthElementObjectReference);
        Destroy(gameObjectOfThisEnemy);
        
    }
}
