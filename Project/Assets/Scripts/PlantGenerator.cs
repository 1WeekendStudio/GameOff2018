namespace Assets.Scripts
{
    using Data;

    using UnityEngine;

    public class PlantGenerator : MonoBehaviour
    {
        [SerializeField]
        private GameObject defaultPlantPrefab;

        [SerializeField]
        private GameObject defaultPlantGhostPrefab;

        public static PlantGenerator Instance { get; private set; }

        public GameObject CreatePlantGhost(PlantDescription description)
        {
            return GameObject.Instantiate(this.defaultPlantGhostPrefab);
        }

        public GameObject CreatePlant(PlantDescription description)
        {
            return GameObject.Instantiate(this.defaultPlantPrefab);
        }

        private void Awake()
        {
            Debug.Assert(PlantGenerator.Instance == null);
            PlantGenerator.Instance = this;
        }
    }
}