namespace Data
{
    [UnityEngine.CreateAssetMenu(fileName = "Data", menuName = "SoilDescription")]
    public class SoilDescription : UnityEngine.ScriptableObject
    {
        public SoilVisualAffinity VisualAffinity;
        public int InitialWaterLevel;
        public int QualityLevel;
    }
}
