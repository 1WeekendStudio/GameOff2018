namespace Data
{
    using UnityEngine;

    public class PlantDescription
    {
        public int LifeTime = 100;
        public int PropagationLevel = 10;
        public int MinimumSunshine = 40;
        public int MaximumSunshine = 60;
        public int MinimumWater = 40;
        public int MaximumWater = 60;
        public int WindResistance = 50;
        public PlantTrait PlantTraits;

        public void Reset(PlantDescription baseDescription)
        {
            this.LifeTime = baseDescription.LifeTime;
            this.PropagationLevel = baseDescription.PropagationLevel;
            this.MinimumSunshine = baseDescription.MinimumSunshine;
            this.MaximumSunshine = baseDescription.MaximumSunshine;
            this.MinimumWater = baseDescription.MinimumWater;
            this.MaximumWater = baseDescription.MaximumWater;
            this.WindResistance = baseDescription.WindResistance;
            this.PlantTraits = baseDescription.PlantTraits;
        }

        public void Apply(Dna dna)
        {
            foreach (var modifier in dna.Modifiers)
            {
                switch (modifier.Property)
                {
                    case Property.MinWater:
                        this.MinimumWater += modifier.Modifier;
                        break;

                    case Property.MaxWater:
                        this.MaximumWater += modifier.Modifier;
                        break;

                    case Property.MinSunshine:
                        this.MinimumSunshine += modifier.Modifier;
                        break;

                    case Property.MaxSunshine:
                        this.MaximumSunshine += modifier.Modifier;
                        break;

                    case Property.Wind:
                        this.WindResistance += modifier.Modifier;
                        break;

                    case Property.LifeTime:
                        this.LifeTime += modifier.Modifier;
                        break;

                    case Property.Propagation:
                        this.PropagationLevel += modifier.Modifier;
                        break;

                    default:
                        Debug.LogWarning("Can't apply modifier on property " + modifier.Property);
                        break;
                }
            }
        }
    }
}
