using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum StapleState
{
    Rest,
    MoveDown,
    MoveUp
}

public class StapleMachine : MonoBehaviour
{
    private StapleState stapleState;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public PlantType machineType;


    [SerializeField]
    private GameObject staple;
    private float extendSpeed;
    private float retractSpeed;

    public void Start()
    {
        staple.GetComponent<ToolType>().SetType(PlantType.Staple);

        machineType = PlantType.Staple;
        stapleState = StapleState.Rest;

        extendSpeed = 0.2f;
        retractSpeed = 0.1f;
        startPosition = gameObject.transform.position;
        endPosition = new Vector3(startPosition.x, startPosition.y - 1.5f, startPosition.z);
    }

    // Move staple towards endpoint
    public void Update()
    {
        switch (stapleState)
        {
            case StapleState.MoveDown:
                transform.position = Vector3.MoveTowards(transform.position, endPosition, extendSpeed);

                if (transform.position.y <= endPosition.y)
                {
                    gameObject.transform.position = endPosition;
                    stapleState = StapleState.MoveUp;
                }
                break;

            case StapleState.MoveUp:
                transform.position = Vector3.MoveTowards(transform.position, startPosition, retractSpeed);

                if (transform.position.y >= startPosition.y)
                {
                    gameObject.transform.position = startPosition;
                    stapleState = StapleState.Rest;
                }
                break;
        }
    }

    // Push the staple down if it isn't down
    public void PressStaple()
    {
        if (stapleState == StapleState.Rest)
        {
            stapleState = StapleState.MoveDown;
        }
    }
}
