using Data;
using System.Collections.Generic;
using UnityEngine;

using View;

public partial class GameManager : MonoBehaviour
{
    [SerializeField]
    private int numberOfDnaToGenerate = 20;

    [SerializeField]
    private string[] dnaNameDatabase;

    private List<Property> availableProperties;

    private Dna[] GenerateDnaDatabase()
    {
        Debug.Assert(this.dnaNameDatabase != null && this.dnaNameDatabase.Length >= this.numberOfDnaToGenerate, "Not enough dna names found.");

        Dna[] dnaDatabase = new Dna[this.numberOfDnaToGenerate];
        this.availableProperties = new List<Property>();
        for (int index = 0; index < dnaDatabase.Length; index++)
        {
            dnaDatabase[index] = this.GenerateDna(this.dnaNameDatabase[index]);
        }

        return dnaDatabase;
    }

    private Dna GenerateDna(string name)
    {
        PropertyModifier[] modifiers = new PropertyModifier[3];
        this.availableProperties.Clear();
        this.availableProperties.Add(Property.MinWater);
        this.availableProperties.Add(Property.MinWater);
        this.availableProperties.Add(Property.MinSunshine);
        this.availableProperties.Add(Property.MaxSunshine);
        this.availableProperties.Add(Property.Wind);
        this.availableProperties.Add(Property.LifeTime);
        this.availableProperties.Add(Property.Propagation);

        for (int index = 0; index < modifiers.Length; index++)
        {
            modifiers[index] = this.GenerateModifier();
        }

        return new Dna(name, DnaState.Unknown, Traits.None, modifiers);
    }

    private PropertyModifier GenerateModifier()
    {
            //Water, Sun, Wind, LifeTime, Propagation

     //   Property targetedProperty = (Property)Random.Range(0, (int)Property.Count);
        int propertyIndex = Random.Range(0, this.availableProperties.Count);
        Property targetedProperty = this.availableProperties[propertyIndex];
        this.availableProperties.RemoveAt(propertyIndex);
        int strengthMultiplier = Random.Range(1,3);
        bool positiveImpact = (Random.Range(0,100) > 50);
        int sign = 1;

        if (positiveImpact) 
        {
            if (targetedProperty == Property.MinWater || targetedProperty == Property.MinSunshine)
                sign = -1;

        }
        else 
        {
            if (!(targetedProperty == Property.MinWater || targetedProperty == Property.MinSunshine))
                sign = -1;
        }

        //Debug.Log("Property: " + targetedProperty + " modified by: " + sign * strengthMultiplier * 10);
        return new PropertyModifier(targetedProperty, sign * strengthMultiplier * 10);
    }

    private Garden CreateGarden(Data.GardenDescription description)
    {
        Debug.Log($"Generate garden: {description.GardenSceneName}");

        PlotGizmo[] plotGizmos = GameObject.FindObjectsOfType<PlotGizmo>();
        Garden garden = new Garden(plotGizmos.Length);

        for (int index = 0; index < plotGizmos.Length; index++)
        {
            garden.Plots[index] = this.CreatePlot(plotGizmos[index]);
        }

        return garden;
    }

    private Plot CreatePlot(PlotGizmo plotGizmo)
    {
        Debug.Log($"Generate plot of size {plotGizmo.GameplayWidth} per {plotGizmo.GameplayHeight}.");

        // Gameplay
        Plot plot = new Plot(plotGizmo.PlotDescription, plotGizmo.GameplayWidth, plotGizmo.GameplayHeight);

        for (int x = 0; x < plot.Width; x++)
        {
            for (int y = 0; y < plot.Height; y++)
            {
                plot.Soil[x, y].Description = plotGizmo.PlotDescription.DefaultSoilDescription;
            }
        }

        // TODO: Generate plot with different type of soil.

        plot.Initialize();

        // View
        View.PlotView view = GameObject.Instantiate(this.plotView, plotGizmo.transform.position, plotGizmo.transform.localRotation);
        view.Plot = plot;
        view.VisualSize = plotGizmo.VisualSize;

        view.Initialize();

        return plot;
    }
}
