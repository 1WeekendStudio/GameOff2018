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
                this.Soil[x, y].Initialize(this.Description.InitialSunshineLevel, this.Description.InitialWindLevel);
            }
        }
    }
}
