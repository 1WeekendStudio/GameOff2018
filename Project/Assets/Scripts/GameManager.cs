using Data;

using UnityEngine;

using View;

public partial class GameManager : MonoBehaviour
{
    [SerializeField]
    private Data.GardenDescription gardenToLoad;
    
    [SerializeField]
    private View.PlotView plotView;

    [SerializeField]
    private int numberOfDnaInInventoryAtStart = 3;

    [SerializeField]
    private float durationBetweenTwoGameplayTicks = 1f;

    private int tickIndex;
    private float lastTickTime = 0f;

    public static GameManager Instance { get; private set; }

    public bool IsLoaded { get; private set; }

    public Garden Garden { get; private set; }

    public Inventory Inventory { get; private set; }

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

        // Generate DNA.
        Dna[] dnaDatabase = this.GenerateDnaDatabase();

        Debug.Assert(dnaDatabase.Length > this.numberOfDnaInInventoryAtStart, "Not enough dna");

        // Put some in inventory.
        this.Inventory = new Inventory();
        for (int index = 0; index < this.numberOfDnaInInventoryAtStart; index++)
        {
            this.Inventory.Add(dnaDatabase[index]);
        }

        // Put the rest in garden.
        for (int index = this.numberOfDnaInInventoryAtStart; index < dnaDatabase.Length; index++)
        {
            Plot plot = this.Garden.Plots[Random.Range(0, this.Garden.Plots.Length)];
            Position position = Position.Random(plot.Width, plot.Height);
            if (plot.Soil[position.X, position.Y].Dna != null)
            {
                index--;
                continue;
            }

            plot.Soil[position.X, position.Y].Dna = dnaDatabase[index];
        }

        this.IsLoaded = true;
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
