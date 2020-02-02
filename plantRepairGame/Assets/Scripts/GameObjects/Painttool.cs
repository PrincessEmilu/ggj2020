using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PaintState
{
    Rest,
    MoveUp,
    MoveDown
}

public class Painttool : MonoBehaviour
{
    private PaintState paintState;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public PlantType machineType;

    [SerializeField]
    private GameObject paintBox;
    private float extendSpeed;
    private float retractSpeed;

    public void Start()
    {
        paintBox.GetComponent<ToolType>().SetType(PlantType.Paint);

        machineType = PlantType.Paint;
        paintState = PaintState.Rest;

        extendSpeed = 0.1f;
        retractSpeed = 0.2f;
        startPosition = transform.position;
        endPosition = new Vector3(startPosition.x, startPosition.y - 1.5f, startPosition.z);
    }

    // Move staple towards endpoint
    public void Update()
    {
        switch (paintState)
        {
            case PaintState.MoveDown:
                transform.position = Vector3.MoveTowards(transform.position, endPosition, extendSpeed);

                if (transform.position.y <= endPosition.y)
                {
                    transform.position = endPosition;
                    paintState = PaintState.MoveUp;
                }
                break;

            case PaintState.MoveUp:
                transform.position = Vector3.MoveTowards(transform.position, startPosition, retractSpeed);

                if (transform.position.y >= startPosition.y)
                {
                    transform.position = startPosition;
                    paintState = PaintState.Rest;
                }
                break;
        }
    }

    // Push the staple down if it isn't down
    public void PressPaint()
    {
        if (paintState == PaintState.Rest)
        {
            paintState = PaintState.MoveDown;
        }
    }
}
