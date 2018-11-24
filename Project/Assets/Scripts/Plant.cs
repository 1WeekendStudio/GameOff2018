public class Plant
{
    public string Name { get; private set; }

    public int Growth { get; private set; }

    public SoilTile SoilTile { get; private set; }

    public Data.PlantDescription Description { get; private set; }

    public Plant(string name, Data.PlantDescription description, SoilTile soilTile)
    {
        this.Name = name;
        this.Description = description;
        this.SoilTile = soilTile;
        this.Growth = 0;
    }

    /**
     * Check if water level is in acceptable range
     **/
    public bool IsWaterOk()
    {
        // TODO also check this.Descriptions.PlantTraits
        return this.SoilTile.WaterLevel > this.Description.MinimumWater && this.SoilTile.WaterLevel < this.Description.MaximumWater;
    }

    /**
     * Check if sunshine level is in acceptable range
     **/
    public bool IsSunshineOk()
    {
        // TODO also check this.Descriptions.PlantTraits
        return this.SoilTile.SunshineLevel > this.Description.MinimumSunshine && this.SoilTile.SunshineLevel < this.Description.MaximumSunshine;
    }

    /**
     * Check if sunshine level is in acceptable range
     **/
    public bool IsWindOk()
    {
        // TODO also check this.Descriptions.PlantTraits
        return this.SoilTile.WindLevel < this.Description.WindResistance;
    }

    /**
     * Check if sunshine level is in acceptable range
     **/
    public bool IsSoilOk()
    {
        // TODO also check this.Descriptions.PlantTraits
        return this.SoilTile.QualityLevel > this.Growth;
    }

    public void Grow()
    {
        this.Growth += 1;
    }
}
