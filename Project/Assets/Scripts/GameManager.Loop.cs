using Data;

using UnityEngine;

using View;

public partial class GameManager : MonoBehaviour
{
    private void Tick()
    {
        this.TickStep_Grow();

        this.TickStep_Propagate();

        this.TickStep_Life();

        this.TickStep_OrganicCreation();
    }

    private void TickStep_Grow()
    {
        foreach (var plot in this.Garden.Plots)
        {
            foreach (var soilTile in plot.Soil)
            {
                var plant = soilTile.Plant;
                if (plant != null)
                {
                    // check sun
                    if (plant.IsWaterOk())
                    {
                        plant.Grow();
                    }
                    // check water
                    if (plant.IsWaterOk())
                    {
                        plant.Grow();
                    }
                    // check soil
                    if (plant.IsSoilOk())
                    {
                        plant.Grow();
                    }

                    soilTile.UpdateQuality(-1 * plant.Growth);
                }
            }
        }
    }

    private void TickStep_Propagate()
    {
        // TODO
    }

    private void TickStep_Life()
    {
        // TODO
    }

    private void TickStep_OrganicCreation()
    {
        // TODO
    }
}
