public class Dna
{
    public Dna(string name, DnaState state, Traits trait, PropertyModifier[] modifiers)
    {
        this.Name = name;
        this.State = state;
        this.Trait = trait;
        this.Modifiers = modifiers;
    }

    public string Name { get; private set; }

    public DnaState State { get; private set; }

    public Traits Trait { get; private set; }

    public PropertyModifier[] Modifiers { get; private set; }
}
