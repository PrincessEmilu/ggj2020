using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public GameObject plantPrefab;
    public List<Vector3> waypointList;

    private List<GameObject> plantList;

    private float beltSpeedStart = 0.05f;
    private float beltSpeed;

    private float spawnTimer;
    private float spawnTimerMax;
    private float spawnTimerMaxStart = 140;


    private int tempScore;
    private int tempLostLives;

    // Init data structures and gameplay variables
    public void Setup()
    {
        plantList = new List<GameObject>();
        tempScore = 0;
        tempLostLives = 0;

        // Default speeds
        spawnTimerMax = spawnTimerMaxStart;
        spawnTimer = spawnTimerMax - 1;
        beltSpeed = beltSpeedStart;
    }

    // Spawn plants based on a timer
    public void SpawnPlants()
    {
        spawnTimer += 1;

        if (spawnTimer >= spawnTimerMax)
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
            GameObject plantObject = plantList[i];
            if (plantObject.GetComponent<Plant>().currentWaypoint == waypointList.Count - 1)
            {

                if (!plantObject.GetComponent<Plant>().isRepaired)
                {
                    tempLostLives++;
                }

                Destroy(plantObject);
                plantList.RemoveAt(i);
                i--;
            }
        }
    }
    public void RemoveAllPlants()
    {
        foreach(GameObject plant in plantList)
        {
            Destroy(plant);
        }

        plantList.Clear();
    }

    public int UpdateScore()
    {
        foreach (GameObject plant in plantList)
        {
            if (plant.GetComponent<Plant>().hasPoint)
            {
                tempScore++;
                plant.GetComponent<Plant>().hasPoint = false;
            }
        }

        int returnValue = tempScore;
        tempScore = 0;
        return returnValue;
    }

    public int UpdateLives()
    {
        int returnValue = tempLostLives;
        tempLostLives = 0;
        return returnValue;
    }

    // Scale speed of production line based on score.
    public void ScaleDifficulty(int score)
    {
        spawnTimerMax = spawnTimerMaxStart - score;
        if (spawnTimerMax < 20)
        {
            spawnTimerMax = 20;
        }

        beltSpeed = beltSpeedStart + (0.007f * (score / 7));
    }
}
