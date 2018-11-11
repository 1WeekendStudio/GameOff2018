public class Garden
{
    public readonly Plot[] Plots;

    public Garden(int numberOfPlots)
    {
        this.Plots = new Plot[numberOfPlots];
    }
}
