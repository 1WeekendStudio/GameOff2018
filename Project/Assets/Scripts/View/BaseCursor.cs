namespace View
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    public abstract class BaseCursor : ICursor
    {
        private Vector3 origin;
        private Vector3 direction;

        public virtual void OnActivate(object parameter)
        {
            this.OnSelect(CursorManager.Instance.SelectedPlot, CursorManager.Instance.SelectedPlotPosition);
        }

        public void Update(Camera camera)
        {
            bool uiControlsInUse = EventSystem.current.IsPointerOverGameObject();
            if (uiControlsInUse)
            {
                return;
            }

            this.origin = camera.transform.position;
            this.direction = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane)) - this.origin;

            RaycastHit hitInfo;
            if (Physics.Raycast(new Ray(this.origin, this.direction), out hitInfo, 1000, LayerMask.NameToLayer("Ground")))
            {
                PlotView plotView = hitInfo.transform.GetComponent<PlotView>();
                Debug.Assert(plotView != null);

                Position tile = plotView.FindNearestTile(hitInfo.point);

                this.Update(camera, plotView, tile);
            }
            else
            {
                this.Update(camera, null, Position.Invalid);
            }
        }

        public abstract void OnSelect(PlotView selectedPlotView, Position selectedTile);

        public abstract void OnDeactivate();

        public virtual void OnDrawGizmos()
        {
            Gizmos.DrawLine(this.origin, this.origin + (1000 * this.direction));
        }

        protected virtual void Update(Camera camera, PlotView hoveredPlotView, Position hoveredTile)
        {
            if (hoveredPlotView != null)
            {
                CursorManager.Instance.HoveredPlot = hoveredPlotView;
                CursorManager.Instance.HoveredPlotPosition = hoveredTile;

                if (Input.GetMouseButtonUp(0))
                {
                    if (hoveredPlotView != CursorManager.Instance.SelectedPlot
                        || hoveredTile != CursorManager.Instance.SelectedPlotPosition)
                    {
                        CursorManager.Instance.SelectedPlot = hoveredPlotView;
                        CursorManager.Instance.SelectedPlotPosition = hoveredTile;

                        this.OnSelect(hoveredPlotView, hoveredTile);
                    }
                }
            }
            else
            {
                CursorManager.Instance.HoveredPlot = null;
                CursorManager.Instance.HoveredPlotPosition = Position.Invalid;

                if (Input.GetMouseButtonUp(0))
                {
                    CursorManager.Instance.SelectedPlot = null;
                    CursorManager.Instance.SelectedPlotPosition = Position.Invalid;

                    this.OnSelect(null, Position.Invalid);
                }
            }
        }
    }
}
