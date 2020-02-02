using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public List<GameObject> listBrokenModels;
    public List<GameObject> listRepairedModels;

    public int currentWaypoint;

    private GameObject brokenModel;
    private GameObject repairedModel;

    private int typeIndex;

    private List<Vector3> waypointList;
    private PlantType plantType;
    public bool isRepaired;
    private bool isBroken;
    public bool hasPoint; // This is a dumb way of checking if the player gets a point from this plant

    private const float safeDistance = 0.01f; // I have no clue what to call this variable

    // Setup plant type
    public void Setup(List<Vector3> waypoints)
    {
        SetPlantType();

        this.waypointList = waypoints;

        currentWaypoint = 0;
        isRepaired = false;
        isBroken = false;

        // Disable fixed model
        repairedModel.SetActive(false);
    }

    // Move towards the next waypoint
    public void MovePlant(float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, waypointList[currentWaypoint + 1], speed);

        // Next waypoint if at the current goal
        if (Vector3.Distance(transform.position,waypointList[currentWaypoint + 1]) < safeDistance)
        {
            currentWaypoint++;
        }
    }

    // Set state of plant based on the tool type of the collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ToolType>().machineType == plantType &&
            !isBroken &&
            !isRepaired)
        {
            brokenModel.SetActive(false);
            repairedModel.SetActive(true);

            isRepaired = true;
            hasPoint = true;
        }

        else
        {
            isBroken = true;
        }
    }

    // Randomly decide plant type
    // Okay, so, this is janky. I know. But let me explain.
    // All plants essentially have the exact same behavior.
    // Legit. There's not change in behavior at all.
    // So, I figured, lets just like, keep a list of all possible models they could use, then when the plant is created, set the appropriate models and get rid of the rest
    // It works. It's janky. Not sure if it is scalable but it seemed the simplest method at the time and game jam time is running out
    private void SetPlantType()
    {
        typeIndex = Random.Range(0, listBrokenModels.Count);
        brokenModel = listBrokenModels[typeIndex];
        repairedModel = listRepairedModels[typeIndex];
        plantType = (PlantType)typeIndex;

        // Destroy models
        for (int i = 0; i < listBrokenModels.Count; i++)
        {
            if (i != typeIndex)
            {
                Destroy(listBrokenModels[i]);
            }
        }

        // Destroy models
        for (int i = 0; i < listBrokenModels.Count; i++)
        {
            if (i != typeIndex)
            {
                Destroy(listRepairedModels[i]);
            }
        }

        // Clear the lists we don't need them
        listBrokenModels.Clear();
        listRepairedModels.Clear();
    }
}
