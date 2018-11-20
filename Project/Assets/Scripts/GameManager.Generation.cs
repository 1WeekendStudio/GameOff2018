using Data;

using UnityEngine;

using View;

public partial class GameManager : MonoBehaviour
{
    [SerializeField]
    private int numberOfDnaToGenerate = 10;

    [SerializeField]
    private string[] dnaNameDatabase;

    private Dna[] GenerateDnaDatabase()
    {
        Debug.Assert(this.dnaNameDatabase != null && this.dnaNameDatabase.Length >= this.numberOfDnaToGenerate, "Not enough dna names found.");

        Dna[] dnaDatabase = new Dna[this.numberOfDnaToGenerate];
        for (int index = 0; index < dnaDatabase.Length; index++)
        {
            dnaDatabase[index] = this.GenerateDna(this.dnaNameDatabase[index]);
        }

        return dnaDatabase;
    }

    private Dna GenerateDna(string name)
    {
        PropertyModifier[] modifiers = new PropertyModifier[3];

        for (int index = 0; index < modifiers.Length; index++)
        {
            modifiers[index] = this.GenerateModifier();
        }

        return new Dna(name, DnaState.Unknown, Traits.None, modifiers);
    }

    private PropertyModifier GenerateModifier()
    {
        return new PropertyModifier((Property)Random.Range(0, (int)Property.Count), Random.Range(-2, 2));
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
