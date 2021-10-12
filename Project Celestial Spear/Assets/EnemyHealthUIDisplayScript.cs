using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class EnemyHealthUIDisplayScript : MonoBehaviour
{
    public RectTransform rectTransformOfTheUIHealthDisplayObject;
    public TextMeshProUGUI textMeshProOfTheUIElement;

    public Transform transformOfThisEnemy;

    public EnemyStatusScript enemyStatusScriptReference;

    public Camera mainCamera;

    public float verticalOffsetOfTheTextFromTheHeadOfTheEnemy;

    private Vector3 positionOfTheTextElementToBeAssigned;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTheHealthNumberOnTheDisplayElement();
        UpdateThePositionOfTheUIHealthDisplayElement();
    }

    private  void UpdateThePositionOfTheUIHealthDisplayElement()
    {
        positionOfTheTextElementToBeAssigned = AddTheVerticalOffsetToThePosition(transformOfThisEnemy.position, verticalOffsetOfTheTextFromTheHeadOfTheEnemy);

        rectTransformOfTheUIHealthDisplayObject.transform.position = mainCamera.WorldToScreenPoint(positionOfTheTextElementToBeAssigned);
    }

    private Vector3 AddTheVerticalOffsetToThePosition(Vector3 position, float offset)
    {
        position.y = position.y + offset;
        return position;
    }

    private void UpdateTheHealthNumberOnTheDisplayElement()
    {
        textMeshProOfTheUIElement.text = ChangeTheHealthDataTypeFromIntToString(enemyStatusScriptReference.health);
    }

    private string ChangeTheHealthDataTypeFromIntToString(int health)
    {
        string healthConvertedToString = "" +  health;
        return healthConvertedToString;
    }

}
