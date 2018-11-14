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
                CursorManager.Instance.HoveredPlot = hoveredPlotView.Plot;
                CursorManager.Instance.HoveredPlotPosition = hoveredTile;
            }
            else
            {
                CursorManager.Instance.HoveredPlot = null;
                CursorManager.Instance.HoveredPlotPosition = Position.Invalid;
            }
        }
    }
}
