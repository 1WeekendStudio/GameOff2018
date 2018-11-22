namespace UI
{
    using System.Collections;

    using Data;

    using UnityEngine;
    using UnityEngine.UI;

    using View;

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
        private Gauge windProperty;

        [SerializeField]
        private Text elevationProperty;

        [SerializeField]
        private float verticalOffset = 150f;

        private Camera camera;

        public void CreatePlant()
        {
            //PlantDescription description = new PlantDescription();
            //    description.LifeTime = 100;
            //    GameManager.Instance.PlantInPlot(hoveredPlotView.Plot, hoveredTile, description);
        }

        private IEnumerator Start()
        {
            while (this.camera == null)
            {
                this.camera = FindObjectOfType<Camera>();
                yield return null;
            }
        }
        
        private void Update()
        {
            if (CursorManager.Instance == null || CursorManager.Instance.SelectedPlot == null)
            {
                this.panel.gameObject.SetActive(false);
                return;
            }

            Position position = CursorManager.Instance.SelectedPlotPosition;

            PlotView plotView = CursorManager.Instance.SelectedPlot;
            SoilTile soilTile = plotView.Plot.Soil[position.X, position.Y];

            bool creationMode = false;

            if (soilTile.Plant == null)
            {
                if (CursorManager.Instance.CurrentCursor is GardeningCursor)
                {
                    // Creation mode.
                    creationMode = true;
                }
                else
                {
                    this.panel.gameObject.SetActive(false);
                    return;
                }
            }

            // Position.
            float yOffset = this.verticalOffset;
            Vector3 tilePosition = plotView.GetTilePosition(position);
            Vector3 screepPoint = this.camera.WorldToScreenPoint(tilePosition);
            this.panel.anchoredPosition = new Vector2(screepPoint.x, yOffset + screepPoint.y);

            this.panel.gameObject.SetActive(true);

            if (creationMode)
            {
                this.title.text = "Create plant";
                this.waterProperty.Value = soilTile.WaterLevel;
                this.waterProperty.DisplayMinMax = false;

                this.sunProperty.Value = soilTile.SunshineLevel;
                this.sunProperty.DisplayMinMax = false;

                this.windProperty.Value = soilTile.WindLevel;
                this.windProperty.DisplayMax = false;

                this.elevationProperty.text = soilTile.Elevation.ToString();
            }
            else
            {
                this.title.text = soilTile.Plant.Name;
                this.waterProperty.Value = soilTile.WaterLevel;
                this.waterProperty.DisplayMinMax = true;
                this.waterProperty.Min = soilTile.Plant.Description.MinimumWater;
                this.waterProperty.Max = soilTile.Plant.Description.MaximumWater;

                this.sunProperty.Value = soilTile.SunshineLevel;
                this.sunProperty.DisplayMinMax = true;
                this.sunProperty.Min = soilTile.Plant.Description.MinimumSunshine;
                this.sunProperty.Max = soilTile.Plant.Description.MaximumSunshine;

                this.windProperty.Value = soilTile.WindLevel;
                this.windProperty.DisplayMax = true;
                this.windProperty.Max = soilTile.Plant.Description.WindResistance;

                this.elevationProperty.text = soilTile.Elevation.ToString();
            }
        }
    }
}
