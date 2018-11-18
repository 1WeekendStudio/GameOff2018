namespace UI
{ 
    using UnityEngine;
    using UnityEngine.UI;

    public class PlantTooltip : MonoBehaviour
    {
        [SerializeField]
        private RectTransform panel;

        [SerializeField]
        private Text title;

        [SerializeField]
        private Gauge waterProperty;

        [SerializeField]
        private Gauge sunProperty;

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

            this.waterProperty.Value = soilTile.WaterLevel;
            this.waterProperty.Min = soilTile.Plant.Description.MinimumWater;
            this.waterProperty.Max = soilTile.Plant.Description.MaximumWater;

            this.sunProperty.Value = soilTile.SunshineLevel;
            this.sunProperty.Min = soilTile.Plant.Description.MinimumSunshine;
            this.sunProperty.Max = soilTile.Plant.Description.MaximumSunshine;

            this.windProperty.text = $"{soilTile.WindLevel} / {soilTile.Plant.Description.WindResistance}";
            this.elevationProperty.text = soilTile.Elevation.ToString();
        }
    }
}
