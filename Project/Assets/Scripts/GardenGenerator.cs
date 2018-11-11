using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenGenerator : MonoBehaviour
{
    public static GardenGenerator Instance { get; private set; }
    
    private void Awake()
    {
        Debug.Assert(GardenGenerator.Instance == null);
        GardenGenerator.Instance = this;
    }

    
}
