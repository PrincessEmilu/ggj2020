using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int currentWaypoint;

    private List<Vector3> waypointList;
    private PlantType plantType;
    private bool isRepaired;
    private bool isBroken;

    private const float safeDistance = 0.01f; // I have no clue what to call this variable

    // Setup plant type
    public void Setup(List<Vector3> waypoints)
    {
        // TODO: A different way to set plant type?
        plantType = PlantType.Staple;

        this.waypointList = waypoints;

        currentWaypoint = 0;
        isRepaired = false;
        isBroken = false;
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
        if (other.gameObject.GetComponent<Staple>().machineType == plantType &&
            !isBroken)
        {
            isRepaired = true;
            Debug.Log("Plant repaired");
        }

        else
        {
            isBroken = true;
            Debug.Log("Plant broken");
        }
    }
}
