using Data;

using UnityEngine;

using View;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Data.GardenDescription gardenToLoad;
    
    [SerializeField]
    private View.PlotView plotView;

    [SerializeField]
    private float durationBetweenTwoGameplayTicks = 1f;

    private int tickIndex;
    private float lastTickTime = 0f;

    public static GameManager Instance { get; private set; }

    public bool IsLoaded { get; private set; }

    public Garden Garden { get; private set; }

    public bool PlantInPlot(Plot plot, Position tile, PlantDescription description)
    {
        if (plot.Soil[tile.X, tile.Y].Plant != null)
        {
            return false;
        }

        plot.Soil[tile.X, tile.Y].Plant = new Plant("PlantName", description);

        CursorManager.Instance.ChangeCursor<DefaultCursor>();

        return true;
    }

    private void Awake()
    {
        Debug.Assert(GameManager.Instance == null);
        GameManager.Instance = this;
    }

    private void OnDestroy()
    {
        GameManager.Instance = null;
    }

    private System.Collections.IEnumerator Start()
    {
        if (this.gardenToLoad == null)
        {
            Debug.LogError("Please define a garden description to load in the game component.");
            yield break;
        }

        // Load garden scene.
        UnityEngine.SceneManagement.SceneManager.LoadScene(this.gardenToLoad.GardenSceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);

        // Wait one frame so Unity can terminate the scene loading.
        yield return null;

        this.Garden = this.CreateGarden(this.gardenToLoad);
        
        this.IsLoaded = true;
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

    private void Update()
    {
        if (!this.IsLoaded)
        {
            return;
        }

        float time = Time.time;
        if (time >= this.lastTickTime + this.durationBetweenTwoGameplayTicks)
        {
            this.tickIndex++;

            this.Tick();

            this.lastTickTime = time;
        }
    }

    private void Tick()
    {
        this.TickStep_Grow();

        this.TickStep_Propagate();

        this.TickStep_Life();

        this.TickStep_OrganicCreation();
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        GUILayout.Label($"Tick {this.tickIndex}");
    }
#endif

    private void TickStep_Grow()
    {
        // TODO
    }

    private void TickStep_Propagate()
    {
        // TODO
    }

    private void TickStep_Life()
    {
        // TODO
    }

    private void TickStep_OrganicCreation()
    {
        // TODO
    }
}
