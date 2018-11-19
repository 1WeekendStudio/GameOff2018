namespace View
{
    using System;
    using Assets.Scripts;

    using Data;

    using UnityEngine;

    public class PlantPlacementCursor : BaseCursor
    {
        private GameObject ghost;

        private PlantDescription plantDescription;

        public override void OnActivate(object parameter)
        {
            this.plantDescription = parameter as PlantDescription;
            Debug.Assert(this.plantDescription != null, "Wrong type of cursor parameter");

            this.ghost = PlantGenerator.Instance.CreatePlantGhost(this.plantDescription);
        }

        public override void OnDeactivate()
        {
            this.ghost.SetActive(false);
        }

        protected override void Update(Camera camera, PlotView hoveredPlotView, Position hoveredTile)
        {
            if (hoveredPlotView != null)
            {
                if (hoveredPlotView.Plot.Soil[hoveredTile.X, hoveredTile.Y].Plant != null)
                {
                    // There is already a plant.
                    this.ghost.SetActive(false);
                    return;
                }

                Vector3 position = hoveredPlotView.GetTilePosition(hoveredTile);
                this.ghost.transform.position = position;
                this.ghost.SetActive(true);

                if (Input.GetMouseButton(0))
                {
                    PlantDescription description = new PlantDescription();
                    description.LifeTime = 100;
                    GameManager.Instance.PlantInPlot(hoveredPlotView.Plot, hoveredTile, description);
                }
            }
            else
            {
                this.ghost.SetActive(false);
            }
        }
    }
}
