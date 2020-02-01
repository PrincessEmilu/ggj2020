using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGlob : MonoBehaviour
{
    public PlantType machineType = PlantType.Water;

    private void Start()
    {
        GetComponent<ToolType>().SetType(PlantType.Water);
    }

    // Destroy if too far down
    void Update()
    {
        if (transform.position.y < -1.5)
        {
            Destroy(gameObject);
        }
    }
}
