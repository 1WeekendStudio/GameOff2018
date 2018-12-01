namespace UI
{
    using System.Collections;
    using System.Collections.Generic;

    using Data;

    using UnityEngine;
    using UnityEngine.UI;

    using View;

    public class PlantMenu : MonoBehaviour
    {
        [SerializeField]
        private RectTransform panel;

        [SerializeField]
        private RectTransform inventoryPanel;

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
        private Button createButton;

        [SerializeField]
        private float verticalOffset = 150f;

        [SerializeField]
        private GameObject recipePanel;

        [SerializeField]
        private RectTransform inventoryContentPanel;

        [SerializeField]
        private GameObject dnaIconPrefab;

        private Camera camera;
        private List<InventoryElement> inventoryElements = new List<InventoryElement>();
        private PlantDescription plantDescription = new PlantDescription();
        private PlantDescription basePlantDescription = new PlantDescription();

        private InventoryElement[] recipeElements;

        public void CreatePlant()
        {
            GameManager.Instance.PlantInPlotWithSelectedDna(CursorManager.Instance.SelectedPlot.Plot, CursorManager.Instance.SelectedPlotPosition);
            CursorManager.Instance.ChangeCursor<DefaultCursor>();
        }

        private IEnumerator Start()
        {
            this.recipeElements = this.recipePanel.GetComponentsInChildren<InventoryElement>();

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
            this.createButton.gameObject.SetActive(creationMode);

            this.waterProperty.Value = soilTile.WaterLevel;
            this.sunProperty.Value = soilTile.SunshineLevel;
            this.windProperty.Value = soilTile.WindLevel;
            this.elevationProperty.text = soilTile.Elevation.ToString();

            if (creationMode)
            {
                this.title.text = "New plant";

                this.plantDescription.Reset(this.basePlantDescription);

                foreach (var dna in GameManager.Instance.Inventory.Dna)
                {
                    if (dna.Selected)
                    {
                        this.plantDescription.Apply(dna);
                    }
                }

                this.UpdateDescription(this.plantDescription);

                this.inventoryPanel.gameObject.SetActive(true);
                this.UpdateInventory();
            }
            else
            {
                this.title.text = soilTile.Plant.Name;

                this.UpdateDescription(soilTile.Plant.Description);

                this.inventoryPanel.gameObject.SetActive(false);
            }
        }

        private void UpdateDescription(PlantDescription description)
        {
            this.waterProperty.Min = description.MinimumWater;
            this.waterProperty.Max = description.MaximumWater;

            this.sunProperty.Min = description.MinimumSunshine;
            this.sunProperty.Max = description.MaximumSunshine;

            this.windProperty.Max = description.WindResistance;

            for (int index = 0; index < description.DnaTraits.Count; index++)
            {
                Dna dna = description.DnaTraits[index];
                this.recipeElements[index].Element = dna;
            }

            for (int index = description.DnaTraits.Count; index < 3; index++)
            {
                this.recipeElements[index].Element = null;
            }
        }

        private void UpdateInventory()
        {
            int dnaIndex;
            for (dnaIndex = 0; dnaIndex < GameManager.Instance.Inventory.Dna.Count; dnaIndex++)
            {
                Dna dna = GameManager.Instance.Inventory.Dna[dnaIndex];

                InventoryElement element = null;
                if (dnaIndex < this.inventoryElements.Count)
                {
                    element = this.inventoryElements[dnaIndex];
                    element.gameObject.SetActive(true);
                }
                else
                {
                    var elementObject = GameObject.Instantiate(this.dnaIconPrefab);
                    element = elementObject.GetComponent<InventoryElement>();
                    element.gameObject.transform.SetParent(this.inventoryContentPanel.gameObject.transform);
                    this.inventoryElements.Add(element);
                }

                element.Element = dna;
            }

            for (int index = dnaIndex; index < this.inventoryElements.Count; index++)
            {
                this.inventoryElements[index].gameObject.SetActive(false);
            }
        }
    }
}
