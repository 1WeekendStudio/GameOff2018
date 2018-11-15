using UnityEngine;
using UnityEngine.UI;

public class PlantTooltip : MonoBehaviour
{
    [SerializeField]
    private RectTransform panel;

    [SerializeField]
    private Text title;

    [SerializeField]
    private Text waterProperty;

    [SerializeField]
    private Text sunProperty;

    [SerializeField]
    private Text windProperty;

    [SerializeField]
    private Text elevationProperty;

    private void Start()
    {
    }
    
    private void Update()
    {
        if (CursorManager.Instance == null || CursorManager.Instance.HoveredPlot == null)
        {
            this.panel.gameObject.SetActive(false);
            return;
        }

        Position position = CursorManager.Instance.HoveredPlotPosition;

        SoilTile soilTile = CursorManager.Instance.HoveredPlot.Soil[position.X, position.Y];

        if (soilTile.Plant == null)
        {
            this.panel.gameObject.SetActive(false);
            return;
        }

        this.panel.gameObject.SetActive(true);
        this.title.text = position.ToString();
        this.waterProperty.text = $"{soilTile.WaterLevel} / {soilTile.Plant.Description.MinimumWater} - {soilTile.Plant.Description.MaximumWater}";
        this.sunProperty.text = $"{soilTile.SunshineLevel} / {soilTile.Plant.Description.MinimumSunshine} - {soilTile.Plant.Description.MaximumSunshine}";
        this.windProperty.text = $"{soilTile.WindLevel} / {soilTile.Plant.Description.WindResistance}";
        this.elevationProperty.text = soilTile.Elevation.ToString();
    }
}
