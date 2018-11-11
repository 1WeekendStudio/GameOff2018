namespace Data
{
    [UnityEngine.CreateAssetMenu(fileName = "Data", menuName = "PlotDescription")]
    public class PlotDescription : UnityEngine.ScriptableObject
    {
        public SoilDescription DefaultSoilDescription;
        public int InitialSunshineLevel;
        public int InitialWindLevel;
    }
}
