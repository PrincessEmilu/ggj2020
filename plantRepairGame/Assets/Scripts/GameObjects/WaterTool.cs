using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTool : MonoBehaviour
{
    public GameObject waterPrefab;

    public void SpawnWater()
    {
        Instantiate(waterPrefab, gameObject.transform.position, Quaternion.identity);
    }
}
