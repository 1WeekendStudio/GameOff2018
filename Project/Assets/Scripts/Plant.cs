public class Plant
{
    public PlantDescription Description { get; private set; }

    public Plant(PlantDescription description)
    {
        this.Description = description;
    }
}
