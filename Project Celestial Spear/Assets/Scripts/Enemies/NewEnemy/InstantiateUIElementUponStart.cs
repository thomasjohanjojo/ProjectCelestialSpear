using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstantiateUIElementUponStart : MonoBehaviour
{

    public EnemyHealthUIDisplayScript enemyHealthUIDisplayScriptReference;

    public GameObject enemyHealthElementPrefab;

    public Canvas canvasReference;

    public Camera mainCamera;

    public GameObject enemyHealthElementObjectReference;

    private RectTransform rectTransformOfTheUIElement;

    private TextMeshProUGUI textMeshProOfTheUIElement;

    
    

    // Start is called before the first frame update
    void Start()
    {
        enemyHealthElementObjectReference = Instantiate(enemyHealthElementPrefab, canvasReference.transform);
        enemyHealthUIDisplayScriptReference.mainCamera = mainCamera;

        textMeshProOfTheUIElement = enemyHealthElementObjectReference.GetComponent<TextMeshProUGUI>();
        rectTransformOfTheUIElement = enemyHealthElementObjectReference.GetComponent<RectTransform>();

        enemyHealthUIDisplayScriptReference.rectTransformOfTheUIHealthDisplayObject = rectTransformOfTheUIElement;
        enemyHealthUIDisplayScriptReference.textMeshProOfTheUIElement = textMeshProOfTheUIElement;
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
