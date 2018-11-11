public class Plant
{
    public Data.PlantDescription Description { get; private set; }

    public Plant(Data.PlantDescription description)
    {
        this.Description = description;
    }
}
