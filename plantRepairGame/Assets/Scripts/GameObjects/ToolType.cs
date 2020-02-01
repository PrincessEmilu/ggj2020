using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolType : MonoBehaviour
{
    public PlantType machineType;

    public void SetType(PlantType type)
    {
        this.machineType = type;
    }
}
