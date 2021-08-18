using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class EnemyHealthUIDisplayScript : MonoBehaviour
{
    public RectTransform rectTransformOfTheUIHealthDisplayObject;
    public TextMeshProUGUI textMeshProOfTheUIElement;

    public Camera mainCamera;

    public float verticalOffsetOfTheTextFromTheHeadOfTheEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rectTransformOfTheUIHealthDisplayObject.transform.position = mainCamera.WorldToScreenPoint(transform.position);
    }
}
