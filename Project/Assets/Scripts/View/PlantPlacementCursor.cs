namespace View
{
    using Assets.Scripts;

    using Data;

    using UnityEngine;

    public class PlantPlacementCursor : ICursor
    {
        private GameObject ghost;

        private PlantDescription plantDescription;

        private Vector3 origin;
        private Vector3 direction;

        public void OnActivate(object parameter)
        {
            this.plantDescription = parameter as PlantDescription;
            Debug.Assert(this.plantDescription != null, "Wrong type of cursor parameter");

            this.ghost = PlantGenerator.Instance.CreatePlantGhost(this.plantDescription);
        }

        public void Update(Camera camera)
        {
            this.origin = camera.transform.position;
            this.direction = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane)) - this.origin;

            RaycastHit hitInfo;
            if (Physics.Raycast(new Ray(this.origin, this.direction), out hitInfo, 1000, LayerMask.NameToLayer("Ground")))
            {
                PlotView plotView = hitInfo.transform.GetComponent<PlotView>();
                Debug.Assert(plotView != null);

                Position tile = plotView.FindNearestTile(hitInfo.point);
                Vector3 position = plotView.GetTilePosition(tile);
                this.ghost.transform.position = position;
                this.ghost.SetActive(true);

                if (Input.GetMouseButton(0))
                {
                    PlantDescription description = new PlantDescription();
                    description.LifeTime = 100;
                    GameManager.Instance.PlantInPlot(plotView.Plot, tile, description);
                }
            }
            else
            {
                this.ghost.SetActive(false);
            }
        }

        public void OnDeactivate()
        {
        }

        public void OnDrawGizmos()
        {
            Gizmos.DrawLine(this.origin, this.origin + (1000 * this.direction));
        }
    }
}