public class Plot
{
    public Data.PlotDescription Description;

    public readonly SoilTile[,] Soil;

    public Plot(Data.PlotDescription description, int width, int height)
    {
        this.Description = description;
        this.Soil = new SoilTile[width, height];
    }

    public int Width => this.Soil.GetLength(0);

    public int Height => this.Soil.GetLength(1);

    public void Initialize()
    {
        for (int x = 0; x < this.Soil.GetLength(0); x++)
        {
            for (int y = 0; y < this.Soil.GetLength(1); y++)
            {
                this.Soil[x, y].SunshineLevel = this.Description.InitialSunshineLevel;
                this.Soil[x, y].WindLevel = this.Description.InitialWindLevel;
                this.Soil[x, y].WaterLevel = this.Soil[x, y].Description.InitialWaterLevel;
            }
        }
    }
}
