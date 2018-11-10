public class Garden
{
    public readonly SoilTile[,] Soil;

    public Garden(int width, int height)
    {
        this.Soil = new SoilTile[width, height];
    }
}
