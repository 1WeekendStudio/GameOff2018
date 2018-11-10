using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private int gardenWidth = 100;

    [SerializeField]
    private int gardenHeight = 80;

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
        this.Garden = new Garden(this.gardenWidth, this.gardenHeight);
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
