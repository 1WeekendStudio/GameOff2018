using UnityEngine;

public class Dna
{
    public Dna(string name, Sprite icon, DnaState state, Traits trait, PropertyModifier[] modifiers)
    {
        this.Name = name;
        this.State = state;
        this.Trait = trait;
        this.Modifiers = modifiers;
        this.Icon = icon;
    }

    public string Name { get; private set; }

    public Sprite Icon { get; private set; }

    public DnaState State { get; set; }

    public Traits Trait { get; private set; }

    public PropertyModifier[] Modifiers { get; private set; }
}
