using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public GameObject plantPrefab;
    public List<Vector3> waypointList;
    public float beltSpeed = 0.1f;

    private List<GameObject> plantList;
    private float spawnTimer = 0;
    private float spawnTimerMax = 60;

    public void Setup()
    {
        plantList = new List<GameObject>();
    }

    // Spawn plants based on a timer
    public void SpawnPlants()
    {
        spawnTimer += 1;

        if (spawnTimer == spawnTimerMax)
        {
            GameObject newPlant = Instantiate(plantPrefab, waypointList[0], Quaternion.identity);
            plantList.Add(newPlant);
            newPlant.GetComponent<Plant>().Setup(waypointList);

            spawnTimer = 0;
        }
    }

    // Move each plant by belt speed
    public void MovePlants()
    {
        foreach(GameObject plantObject in plantList)
        {
            Plant plant = plantObject.GetComponent<Plant>();
            plant.MovePlant(beltSpeed);
        }
    }

    // Remove each plant if it is at the last waypoint
    public void RemovePlants()
    {
        for (int i = 0; i < plantList.Count; i++)
        {
            if (plantList[i].GetComponent<Plant>().currentWaypoint == waypointList.Count - 1)
            {
                Destroy(plantList[i]);
                plantList.RemoveAt(i);
                i--;
            }
        }
    }
}
