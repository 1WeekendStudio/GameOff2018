namespace UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class Gauge : MonoBehaviour
    {
        private const float MaxValue = 100f;

        [SerializeField]
        private Image value;

        [SerializeField]
        private Image min;

        [SerializeField]
        private Image max;

        [SerializeField]
        private bool displayMin;

        [SerializeField]
        private bool displayMax;

        private RectTransform rectTransform;

        public int Value
        {
            set
            {
                float position = (value / MaxValue) * this.rectTransform.rect.width;
                this.value.rectTransform.anchoredPosition = new Vector2(position, 0);
            }
        }

        public int Min
        {
            set
            {
                float position = (value / MaxValue) * this.rectTransform.rect.width;
                this.min.rectTransform.anchoredPosition = new Vector2(position, 0);
            }
        }

        public bool DisplayMin
        {
            set
            {
                this.displayMin = value;
                this.min.gameObject.SetActive(this.displayMin);
            }
        }

        public int Max
        {
            set
            {
                float position = (value / MaxValue) * this.rectTransform.rect.width;
                this.max.rectTransform.anchoredPosition = new Vector2(position, 0);
            }
        }

        public bool DisplayMax
        {
            set
            {
                this.displayMax = value;
                this.max.gameObject.SetActive(this.displayMax);
            }
        }

        public bool DisplayMinMax
        {
            set
            {
                this.DisplayMin = value;
                this.DisplayMax = value;
            }
        }

        private void Start()
        {
            this.rectTransform = this.GetComponent<RectTransform>();
            if (!this.displayMin)
            {
                this.min.gameObject.SetActive(false);
            }

            if (!this.displayMax)
            {
                this.max.gameObject.SetActive(false);
            }
        }
        
        private void Update()
        {
        }
    }
}
