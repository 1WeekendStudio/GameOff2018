using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotGenerator : MonoBehaviour
{
    public static PlotGenerator Instance { get; private set; }

    [SerializeField]
    private Data.PlotDescription defaultPlotDescription;

    [SerializeField]
    private Data.SoilDescription defaultSoilDescription;

    private void Awake()
    {
        Debug.Assert(PlotGenerator.Instance == null);
        PlotGenerator.Instance = this;
    }

    public Plot CreatePlot(int width, int height)
    {
        Debug.Log($"Generate plot of size {width} per {height}.");

        Plot plot = new Plot(this.defaultPlotDescription, width, height);

        // TODO: Generate plot with different type of soil.

        for (int x = 0; x < plot.Soil.GetLength(0); x++)
        {
            for (int y = 0; y < plot.Soil.GetLength(1); y++)
            {
                plot.Soil[x, y].Description = this.defaultSoilDescription;
            }
        }

        plot.Initialize();

        return plot;
    }
}
