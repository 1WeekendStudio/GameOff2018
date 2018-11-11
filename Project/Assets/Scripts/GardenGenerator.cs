using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenGenerator : MonoBehaviour
{
    public static GardenGenerator Instance { get; private set; }

    [SerializeField]
    private Data.GardenDescription defaultGardenDescription;

    [SerializeField]
    private Data.SoilDescription defaultSoilDescription;

    private void Awake()
    {
        Debug.Assert(GardenGenerator.Instance == null);
        GardenGenerator.Instance = this;
    }

    public Garden CreateGarden(int width, int height)
    {
        Debug.Log($"Generate garden of size {width} per {height}.");

        Garden garden = new Garden(this.defaultGardenDescription, width, height);

        // TODO: Generate garden with different type of soil.

        for (int x = 0; x < garden.Soil.GetLength(0); x++)
        {
            for (int y = 0; y < garden.Soil.GetLength(1); y++)
            {
                garden.Soil[x, y].Description = this.defaultSoilDescription;
            }
        }

        garden.Initialize();

        return garden;
    }
}
