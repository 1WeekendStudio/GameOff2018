namespace View
{
    using System;
    using Assets.Scripts;

    using Data;

    using UnityEngine;

    public class DefaultCursor : BaseCursor
    {
        public override void OnActivate(object parameter)
        {
        }

        public override void OnDeactivate()
        {
        }

        protected override void Update(Camera camera, PlotView hoveredPlotView, Position hoveredTile)
        {
            if (hoveredPlotView != null)
            {
                CursorManager.Instance.HoveredPlot = hoveredPlotView;
                CursorManager.Instance.HoveredPlotPosition = hoveredTile;

                if (Input.GetMouseButtonUp(0))
                {
                    CursorManager.Instance.SelectedPlot = hoveredPlotView;
                    CursorManager.Instance.SelectedPlotPosition = hoveredTile;
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
                }
            }
        }
    }
}
