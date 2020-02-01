using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int currentWaypoint;

    private List<Vector3> waypointList;
    private PlantType plantType;
    private bool isRepaired;

    private const float safeDistance = 0.5f; // I have no clue what to call this variable

    // Setup plant type
    public void Setup(List<Vector3> waypoints)
    {
        // TODO: A different way to set plant type?
        plantType = PlantType.Staple;

        this.waypointList = waypoints;

        currentWaypoint = 0;
        isRepaired = false;
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
}
