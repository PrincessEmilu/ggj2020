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
        startPosition = paintBox.transform.position;
        endPosition = new Vector3(startPosition.x, startPosition.y - 2.5f, startPosition.z);
    }

    // Move staple towards endpoint
    public void Update()
    {
        switch (paintState)
        {
            case PaintState.MoveDown:
                paintBox.transform.position = Vector3.MoveTowards(paintBox.transform.position, endPosition, extendSpeed);

                if (paintBox.transform.position.y <= endPosition.y)
                {
                    paintBox.transform.position = endPosition;
                    paintState = PaintState.MoveUp;
                }
                break;

            case PaintState.MoveUp:
                paintBox.transform.position = Vector3.MoveTowards(paintBox.transform.position, startPosition, retractSpeed);

                if (paintBox.transform.position.y >= startPosition.y)
                {
                    paintBox.transform.position = startPosition;
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
