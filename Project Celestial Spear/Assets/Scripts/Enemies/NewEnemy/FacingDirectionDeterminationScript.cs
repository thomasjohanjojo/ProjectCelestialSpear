using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingDirectionDeterminationScript : MonoBehaviour
{

    public Transform TransformWhichIndicatesTheFacingSide;
    public Transform TransformWhichIndicatesTheBackSide;

    public float facingDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        facingDirection = CalculateFacingDirectionBySubtractingTheTransforms();
    }


    private float CalculateFacingDirectionBySubtractingTheTransforms()
    {
        if(TransformWhichIndicatesTheFacingSide.position.x > TransformWhichIndicatesTheBackSide.position.x)
        {
            return 1;
        }

        else
        {
            return -1;
        }
    }
}
