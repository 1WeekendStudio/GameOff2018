namespace View
{
    using UnityEngine;

    public class PlantView : MonoBehaviour
    {
        [SerializeField]
        private float lifePercentAtFullSize = 0.3f;

        [SerializeField]
        private float sizeOffset = 0.2f;

        [SerializeField]
        private float sizeRandom = 0.2f;

        private Animator animator;

        private float sizeMultiplier;

        private Vector3 targetSize;

        public Plant Plant
        {
            get;
            set;
        }

        private void Awake()
        {
            this.animator = this.GetComponentInChildren<Animator>();
            Debug.Assert(this.animator != null);

            this.sizeMultiplier = Random.Range(1f - this.sizeRandom, 1f + this.sizeRandom);
        }

        private void Start()
        {
            // Initialize size.
            this.gameObject.transform.localScale = this.ComputeSize();
        }

        private void Update()
        {
            this.targetSize = this.ComputeSize();
            this.gameObject.transform.localScale = Vector3.Lerp(this.gameObject.transform.localScale, this.targetSize, Time.deltaTime);
        }

        private Vector3 ComputeSize()
        {
            var size = Mathf.Clamp01(this.Plant.Growth / (this.lifePercentAtFullSize * this.Plant.Description.LifeTime));
            float scale = Mathf.Clamp01(this.sizeOffset + size);
            return new Vector3(scale, scale, scale) * this.sizeMultiplier;
        }
    }
}
