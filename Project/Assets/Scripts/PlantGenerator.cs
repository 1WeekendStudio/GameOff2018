namespace Assets.Scripts
{
    using Data;

    using UnityEngine;

    using View;

    public class PlantGenerator : MonoBehaviour
    {
        [SerializeField]
        private GameObject defaultPlantPrefab;

        [SerializeField]
        private Color[] waterColors;

        [SerializeField]
        private Color[] sunshineColors;

        [SerializeField]
        private Color[] propagationColors;

        private enum PlantBlendShape
        {
            Leaf,
            Roots,
            Thickness,
            Curvy,
        }

        private enum PlantMaterial
        {
            Bark,
            Leaf,
            Flower,
        }

        public static PlantGenerator Instance { get; private set; }

        public GameObject CreatePlant(Plant plant)
        {
            GameObject plantObject = GameObject.Instantiate(this.defaultPlantPrefab);

            PlantView plantView = plantObject.GetComponent<PlantView>();
            plantView.Plant = plant;

            var meshRenderer = plantObject.GetComponentInChildren<SkinnedMeshRenderer>();

            meshRenderer.SetBlendShapeWeight((int)PlantBlendShape.Thickness, plant.Description.MaximumWater);
            meshRenderer.SetBlendShapeWeight((int)PlantBlendShape.Leaf, plant.Description.MaximumSunshine);
            meshRenderer.SetBlendShapeWeight((int)PlantBlendShape.Roots, plant.Description.WindResistance);
            meshRenderer.SetBlendShapeWeight((int)PlantBlendShape.Curvy, Random.Range(0, 100));

            int colorIndex = Mathf.RoundToInt((plant.Description.MinimumWater / 100f) * this.waterColors.Length);
            meshRenderer.materials[(int)PlantMaterial.Bark].color = this.waterColors[colorIndex];

            colorIndex = Mathf.RoundToInt((plant.Description.MinimumSunshine / 100f) * this.sunshineColors.Length);
            meshRenderer.materials[(int)PlantMaterial.Leaf].color = this.sunshineColors[colorIndex];

            colorIndex = Mathf.RoundToInt((plant.Description.PropagationLevel / 100f) * this.propagationColors.Length);
            meshRenderer.materials[(int)PlantMaterial.Flower].color = this.propagationColors[colorIndex];

            return plantObject;
        }

        private void Awake()
        {
            Debug.Assert(PlantGenerator.Instance == null);
            PlantGenerator.Instance = this;
        }
    }
}