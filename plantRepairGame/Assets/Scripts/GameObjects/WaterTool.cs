using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTool : MonoBehaviour
{
    public GameObject waterPrefab;
    private int cooldown = 0;
    private int cooldownMax = 30;

    void Start()
    {
        
    }
    void Update()
    {
        if (cooldown < cooldownMax)
        {
            cooldown++;
        }
    }

    public void SpawnWater()
    {
        if (cooldown == 30)
        {
            Instantiate(waterPrefab, gameObject.transform.position, Quaternion.identity);
            cooldown = 0;
        }
    }
}
