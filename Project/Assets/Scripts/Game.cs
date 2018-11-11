using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private Data.GardenDescription gardenToLoad;

    [SerializeField]
    private float durationBetweenTwoGameplayTicks = 1f;

    private int tickIndex;
    private float lastTickTime = 0f;

    public static Game Instance { get; private set; }

    public Garden Garden { get; private set; }

    private void Awake()
    {
        Debug.Assert(Game.Instance == null);
        Game.Instance = this;
    }

    private void OnDestroy()
    {
        Game.Instance = null;
    }

    private void Start()
    {
        if (this.gardenToLoad == null)
        {
            Debug.LogError("Please define a garden description to load in the game component.");
            return;
        }

        this.Garden = this.CreateGarden(this.gardenToLoad);
    }

    private Garden CreateGarden(Data.GardenDescription description)
    {
        Debug.Log($"Generate garden: {description.GardenSceneName}");

        UnityEngine.SceneManagement.SceneManager.LoadScene(description.GardenSceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);

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

        Plot plot = new Plot(plotGizmo.PlotDescription, plotGizmo.GameplayWidth, plotGizmo.GameplayHeight);

        for (int x = 0; x < plot.Soil.GetLength(0); x++)
        {
            for (int y = 0; y < plot.Soil.GetLength(1); y++)
            {
                plot.Soil[x, y].Description = plotGizmo.PlotDescription.DefaultSoilDescription;
            }
        }

        // TODO: Generate plot with different type of soil.

        plot.Initialize();

        return plot;
    }

    private void Update()
    {
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
