public class Plant
{
    public string Name { get; private set; }

    public Data.PlantDescription Description { get; private set; }

    public Plant(string name, Data.PlantDescription description)
    {
        this.Name = name;
        this.Description = description;
    }
}
