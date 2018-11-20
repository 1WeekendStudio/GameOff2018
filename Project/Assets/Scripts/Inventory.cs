using System.Collections.Generic;

public class Inventory
{
    public List<Dna> Dna { get; } = new List<Dna>();

    public void Add(Dna dna)
    {
        this.Dna.Add(dna);
    }
}
