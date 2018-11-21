namespace View
{
    using System;
    using Assets.Scripts;

    using Data;

    using UnityEngine;

    public class GardeningCursor : BaseCursor
    {
        public PlantDescription PlantDescription;

        private GameObject hoverObject;
        private GameObject selectionObject;

        public void LoadPrefabs(GameObject hoverPrefab, GameObject selectionPrefab)
        {
            Debug.Assert(hoverPrefab != null, "Missing selection selectionPrefab.");
            this.hoverObject = GameObject.Instantiate(hoverPrefab);

            Debug.Assert(selectionPrefab != null, "Missing selection selectionPrefab.");
            this.selectionObject = GameObject.Instantiate(selectionPrefab);
        }

        public override void OnActivate(object parameter)
        {
        }

        public override void OnDeactivate()
        {
            this.hoverObject.SetActive(false);
            this.selectionObject.SetActive(false);
        }

        public override void OnSelect(PlotView selectedPlotView, Position selectedTile)
        {
            if (selectedPlotView != null)
            {
                if (selectedPlotView.Plot.Soil[selectedTile.X, selectedTile.Y].Plant != null)
                {
                    // There is already a plant.
                    this.selectionObject.SetActive(false);
                    return;
                }

                Vector3 position = selectedPlotView.GetTilePosition(selectedTile);
                this.selectionObject.transform.position = position;
                this.selectionObject.SetActive(true);

                //if (Input.GetMouseButton(0))
                //{
                //    PlantDescription description = new PlantDescription();
                //    description.LifeTime = 100;
                //    GameManager.Instance.PlantInPlot(hoveredPlotView.Plot, hoveredTile, description);
                //}
            }
            else
            {
                this.selectionObject.SetActive(false);
            }
        }

        protected override void Update(Camera camera, PlotView hoveredPlotView, Position hoveredTile)
        {
            base.Update(camera, hoveredPlotView, hoveredTile);

            if (hoveredPlotView != null)
            {
                Vector3 position = hoveredPlotView.GetTilePosition(hoveredTile);
                this.hoverObject.transform.position = position;
                this.hoverObject.SetActive(true);
            }
            else
            {
                this.hoverObject.SetActive(false);
            }
        }
    }
}
