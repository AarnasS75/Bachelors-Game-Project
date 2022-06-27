using UnityEngine;

public class EvaluateResult : MonoBehaviour
{
    public Sprite checkmark;
    public Sprite crossmark;
    public GameObject requiredResult;

    public int finalIntValue;
    public string finalColorValue;

    [Header("Checkmark sprites")]
    public GameObject spalvaCheckmark = null;
    public GameObject intCheckmark = null;
    public GameObject shapeCheckmark = null;
    public GameObject rotationCheckmark = null;

    public Vector3 rotationNeeded;

    public bool evaluateColor, evaluateInt, evaluateShape, evaluateRotation;

    [HideInInspector]
    public SpriteRenderer spalvaRenderer;
    [HideInInspector]
    public SpriteRenderer shapeRenderer;
    [HideInInspector]
    public SpriteRenderer intRenderer;
    [HideInInspector]
    public SpriteRenderer rotationRenderer;

    SpriteRenderer objSprite;
    Sprite sprite;

    public int correctResultCount;

    private void Start()
    {
        GetCheckmarkRenderers();

        objSprite = requiredResult.GetComponent<SpriteRenderer>();
        sprite = objSprite.sprite;
        spalvaRenderer.sprite = null;
        shapeRenderer.sprite = null;
        intRenderer.sprite = null;
        rotationRenderer.sprite = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            GameManager.instance.boxesEvaluated++;

            // Verte
            if(evaluateInt && !evaluateColor && !evaluateShape && !evaluateRotation)
            {
                if (collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    intRenderer.sprite = checkmark;
                    correctResultCount++;
                }
                else
                {
                    intRenderer.sprite = crossmark;
                }
            }
            // Krytpis
            else if (!evaluateInt && !evaluateColor && !evaluateShape && evaluateRotation)
            {
                if (collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z))
                {
                    rotationRenderer.sprite = checkmark;
                    correctResultCount++;
                }
                else
                {
                    rotationRenderer.sprite = crossmark;
                }
            }
            // Spalva
            else if (!evaluateInt && evaluateColor && !evaluateShape && !evaluateRotation)
            {
                if (collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color))
                {
                    spalvaRenderer.sprite = checkmark;
                    correctResultCount++;
                }
                else
                {
                    spalvaRenderer.sprite = crossmark;
                }
            }
            // Shape
            else if (!evaluateInt && !evaluateColor && evaluateShape && !evaluateRotation)
            {
                if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(objSprite.sprite))
                {
                    shapeRenderer.sprite = checkmark;
                    correctResultCount++;
                }
                else
                {
                    shapeRenderer.sprite = crossmark;
                }
            }


            // Verte ir Kryptis
            else if (evaluateInt && !evaluateColor && !evaluateShape && evaluateRotation)
            {
                if (collision.GetComponent<Box>().boxNr.Equals(finalIntValue) &&
                    collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z))
                {
                    rotationRenderer.sprite = checkmark;
                    intRenderer.sprite = checkmark;
                    correctResultCount++;
                }
                else if (!collision.GetComponent<Box>().boxNr.Equals(finalIntValue) &&
                    collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z))
                {
                    rotationRenderer.sprite = checkmark;
                    intRenderer.sprite = crossmark;
                }
                else if (collision.GetComponent<Box>().boxNr.Equals(finalIntValue) &&
                    !collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z))
                {
                    rotationRenderer.sprite = crossmark;
                    intRenderer.sprite = checkmark;
                }
                else
                {
                    rotationRenderer.sprite = crossmark;
                    intRenderer.sprite = crossmark;
                }
            }
            // Spalva ir Kryptis
            else if (!evaluateInt && evaluateColor && !evaluateShape && evaluateRotation)
            {
                if (collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z))
                {
                    spalvaRenderer.sprite = checkmark;
                    rotationRenderer.sprite = checkmark;
                    correctResultCount++;
                }
                else if (!collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z))
                {
                    spalvaRenderer.sprite = crossmark;
                    rotationRenderer.sprite = checkmark;
                }
                else if (collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    !collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z))
                {
                    spalvaRenderer.sprite = checkmark;
                    rotationRenderer.sprite = crossmark;
                }
                else
                {
                    spalvaRenderer.sprite = crossmark;
                    rotationRenderer.sprite = crossmark;
                }
            }
            // Vert? ir Spalva
            else if (evaluateInt && evaluateColor && !evaluateShape && !evaluateRotation)
            {
                if (collision.GetComponent<Box>().boxNr.Equals(finalIntValue) && collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color))
                {
                    spalvaRenderer.sprite = checkmark;
                    intRenderer.sprite = checkmark;
                    correctResultCount++;
                }
                else if (!collision.GetComponent<Box>().boxNr.Equals(finalIntValue) && collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color))
                {
                    spalvaRenderer.sprite = checkmark;
                    intRenderer.sprite = crossmark;
                }
                else if (collision.GetComponent<Box>().boxNr.Equals(finalIntValue) && !collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color))
                {
                    spalvaRenderer.sprite = crossmark;
                    intRenderer.sprite = checkmark;
                }
                else
                {
                    spalvaRenderer.sprite = crossmark;
                    intRenderer.sprite = crossmark;
                }
            }
            // Forma ir Kryptis
            else if (!evaluateInt && !evaluateColor && evaluateShape && evaluateRotation)
            {
                if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(objSprite) && collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z))
                {
                    rotationRenderer.sprite = checkmark;
                    shapeRenderer.sprite = checkmark;
                    correctResultCount++;
                }
                else if (!collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(objSprite) && collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z))
                {
                    rotationRenderer.sprite = checkmark;
                    shapeRenderer.sprite = crossmark;
                }
                else if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(objSprite) && !collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z))
                {
                    rotationRenderer.sprite = crossmark;
                    shapeRenderer.sprite = checkmark;
                }
                else
                {
                    rotationRenderer.sprite = crossmark;
                    shapeRenderer.sprite = crossmark;
                }
            }
            // Spalva ir Forma
            else if (!evaluateInt && evaluateColor && evaluateShape && !evaluateRotation)
            {
                if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) && collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color))
                {
                    correctResultCount++;
                    shapeRenderer.sprite = checkmark;
                    spalvaRenderer.sprite = checkmark;
                }
                else if(!collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) && collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color))
                {
                    spalvaRenderer.sprite = checkmark;
                    shapeRenderer.sprite = crossmark;
                }
                else if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) && !collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color))
                {
                    spalvaRenderer.sprite = crossmark;
                    shapeRenderer.sprite = checkmark;
                }
                else
                {
                    spalvaRenderer.sprite = crossmark;
                    shapeRenderer.sprite = crossmark;
                }
            }

            // Forma, Verte ir Spalva
            else if(evaluateInt && evaluateColor && evaluateShape && !evaluateRotation)
            {
                if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                    collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    correctResultCount++;
                    shapeRenderer.sprite = checkmark;
                    spalvaRenderer.sprite = checkmark;
                    intRenderer.sprite = checkmark;
                }
                else if (!collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                    collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    spalvaRenderer.sprite = checkmark;
                    shapeRenderer.sprite = crossmark;
                    intRenderer.sprite = checkmark;
                }
                else if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                    !collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    spalvaRenderer.sprite = crossmark;
                    shapeRenderer.sprite = checkmark;
                    intRenderer.sprite = checkmark;
                }
                else if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                    collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    !collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    shapeRenderer.sprite = checkmark;
                    spalvaRenderer.sprite = checkmark;
                    intRenderer.sprite = crossmark;
                }
                else if (!collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                    collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    !collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    spalvaRenderer.sprite = checkmark;
                    shapeRenderer.sprite = crossmark;
                    intRenderer.sprite = crossmark;
                }
                else if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                    !collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    !collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    spalvaRenderer.sprite = crossmark;
                    shapeRenderer.sprite = checkmark;
                    intRenderer.sprite = crossmark;
                }
                else
                {
                    spalvaRenderer.sprite = crossmark;
                    shapeRenderer.sprite = crossmark;
                    intRenderer.sprite = crossmark;

                }
            }
            // Kryptis, Verte ir Spalva
            else if (evaluateInt && evaluateColor && !evaluateShape && evaluateRotation)
            {
                if (collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) &&
                    collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    correctResultCount++;
                    rotationRenderer.sprite = checkmark;
                    spalvaRenderer.sprite = checkmark;
                    intRenderer.sprite = checkmark;
                }
                else if (!collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) &&
                    collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    spalvaRenderer.sprite = checkmark;
                    rotationRenderer.sprite = crossmark;
                    intRenderer.sprite = checkmark;
                }
                else if (collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) &&
                    !collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    spalvaRenderer.sprite = crossmark;
                    rotationRenderer.sprite = checkmark;
                    intRenderer.sprite = checkmark;
                }
                else if (collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) &&
                    collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    !collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    rotationRenderer.sprite = checkmark;
                    spalvaRenderer.sprite = checkmark;
                    intRenderer.sprite = crossmark;
                }
                else if (!collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) &&
                    collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    !collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    spalvaRenderer.sprite = checkmark;
                    rotationRenderer.sprite = crossmark;
                    intRenderer.sprite = crossmark;
                }
                else if (collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) &&
                    !collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    !collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    spalvaRenderer.sprite = crossmark;
                    rotationRenderer.sprite = checkmark;
                    intRenderer.sprite = crossmark;
                }
                else
                {
                    spalvaRenderer.sprite = crossmark;
                    rotationRenderer.sprite = crossmark;
                    intRenderer.sprite = crossmark;

                }
            }
            // Only Color, Shape and Rotation values
            else if (!evaluateInt && evaluateColor && evaluateShape && evaluateRotation)
            {
                if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) && 
                    collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z))
                {
                    correctResultCount++;
                    shapeRenderer.sprite = checkmark;
                    spalvaRenderer.sprite = checkmark;
                    rotationRenderer.sprite = checkmark;
                }
                else if (!collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) && 
                    collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z))
                {
                    spalvaRenderer.sprite = checkmark;
                    shapeRenderer.sprite = crossmark;
                    rotationRenderer.sprite = checkmark;
                }
                else if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) && 
                    !collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z))
                {
                    spalvaRenderer.sprite = crossmark;
                    shapeRenderer.sprite = checkmark;
                    rotationRenderer.sprite = checkmark;
                }
                else if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                    collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    !collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z))
                {
                    shapeRenderer.sprite = checkmark;
                    spalvaRenderer.sprite = checkmark;
                    rotationRenderer.sprite = crossmark;
                }
                else if (!collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                    collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    !collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z))
                {
                    spalvaRenderer.sprite = checkmark;
                    shapeRenderer.sprite = crossmark;
                    rotationRenderer.sprite = crossmark;
                }
                else if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                    !collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    !collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z))
                {
                    spalvaRenderer.sprite = crossmark;
                    shapeRenderer.sprite = checkmark;
                    rotationRenderer.sprite = crossmark;
                }
            }


            // All
            else if (evaluateInt && evaluateColor && evaluateShape && evaluateRotation)
            {
                // All true
                if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                    collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) && collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    correctResultCount++;
                    intRenderer.sprite = checkmark;
                    shapeRenderer.sprite = checkmark;
                    spalvaRenderer.sprite = checkmark;
                    rotationRenderer.sprite = checkmark;
                }
                // all false
                else if (!collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                   !collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                   !collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) && !collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    intRenderer.sprite = crossmark;
                    spalvaRenderer.sprite = crossmark;
                    shapeRenderer.sprite = crossmark;
                    rotationRenderer.sprite = crossmark;
                }
                
                
                // Only Shape is true
                else if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                    !collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    !collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) && 
                    !collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    spalvaRenderer.sprite = crossmark;
                    intRenderer.sprite = crossmark;
                    shapeRenderer.sprite = checkmark;
                    rotationRenderer.sprite = crossmark;
                }
                // Only Spalva is true
                else if (!collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                    collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    !collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) && 
                    !collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    spalvaRenderer.sprite = checkmark;
                    intRenderer.sprite = crossmark;
                    shapeRenderer.sprite = crossmark;
                    rotationRenderer.sprite = crossmark;
                }
                // Only Direction is true
                else if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                    !collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    !collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) && 
                    !collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    intRenderer.sprite = crossmark;
                    shapeRenderer.sprite = crossmark;
                    spalvaRenderer.sprite = crossmark;
                    rotationRenderer.sprite = checkmark;
                }
                // Only value is true
                else if (!collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                   !collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                   !collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) && 
                   collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    intRenderer.sprite = checkmark;
                    shapeRenderer.sprite = crossmark;
                    spalvaRenderer.sprite = crossmark;
                    rotationRenderer.sprite = crossmark;
                }
                
                

                // Value and Color are true
                else if (!collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                   collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                   !collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) && 
                   collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    spalvaRenderer.sprite = checkmark;
                    intRenderer.sprite = checkmark;
                    shapeRenderer.sprite = crossmark;
                    rotationRenderer.sprite = crossmark;
                }
                // Value and Shape are true
                else if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                    !collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    !collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) && 
                    collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    intRenderer.sprite = checkmark;
                    spalvaRenderer.sprite = crossmark;
                    shapeRenderer.sprite = checkmark;
                    rotationRenderer.sprite = crossmark;
                }
                // Value and Direction are true
                else if (!collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                   !collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                   collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) && collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    spalvaRenderer.sprite = crossmark;
                    intRenderer.sprite = checkmark;
                    shapeRenderer.sprite = crossmark;
                    rotationRenderer.sprite = checkmark;
                }
                // Color and Direction are true
                else if (!collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                    collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) && !collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    intRenderer.sprite = crossmark;
                    spalvaRenderer.sprite = checkmark;
                    shapeRenderer.sprite = crossmark;
                    rotationRenderer.sprite = checkmark;
                }
                // Color and Shape are true
                else if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                    collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    !collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) && !collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    intRenderer.sprite = crossmark;
                    spalvaRenderer.sprite = checkmark;
                    shapeRenderer.sprite = checkmark;
                    rotationRenderer.sprite = crossmark;
                }
                // Shape and Direction are true
                else if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                    !collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                    collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) && !collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    spalvaRenderer.sprite = crossmark;
                    intRenderer.sprite = crossmark;
                    shapeRenderer.sprite = checkmark;
                    rotationRenderer.sprite = checkmark;
                }




                // Color Shape and Value are true
                else if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                   collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                   !collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) && collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    intRenderer.sprite = checkmark;
                    spalvaRenderer.sprite = checkmark;
                    shapeRenderer.sprite = checkmark;
                    rotationRenderer.sprite = crossmark;
                }
                // Color Shape and Direction are true
                else if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                   collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                   collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) && collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    intRenderer.sprite = crossmark;
                    spalvaRenderer.sprite = checkmark;
                    shapeRenderer.sprite = checkmark;
                    rotationRenderer.sprite = checkmark;
                }
                // Color Value and Direction are true
                else if (!collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                   collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                   collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) && collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    intRenderer.sprite = checkmark;
                    spalvaRenderer.sprite = checkmark;
                    shapeRenderer.sprite = crossmark;
                    rotationRenderer.sprite = checkmark;
                }
                // Value Direction and Shape are true
                else if (collision.GetComponentInChildren<SpriteRenderer>().sprite.Equals(sprite) &&
                   !collision.GetComponentInChildren<SpriteRenderer>().color.Equals(objSprite.color) &&
                   collision.GetComponent<Box>().finalRotation.Equals(rotationNeeded.z) && collision.GetComponent<Box>().boxNr.Equals(finalIntValue))
                {
                    intRenderer.sprite = checkmark;
                    spalvaRenderer.sprite = crossmark;
                    shapeRenderer.sprite = checkmark;
                    rotationRenderer.sprite = checkmark;
                }

            }
        }
    }
    void GetCheckmarkRenderers()
    {
        spalvaRenderer = spalvaCheckmark.GetComponent<SpriteRenderer>();
        intRenderer = intCheckmark.GetComponent<SpriteRenderer>();
        shapeRenderer = shapeCheckmark.GetComponent<SpriteRenderer>();
        rotationRenderer = rotationCheckmark.GetComponent<SpriteRenderer>();
    }
}
