namespace UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class InventoryElement : MonoBehaviour
    {
        private Image image;

        private Dna element;

        public delegate void ElementClickDelegate(Dna selectedDna);

        public event ElementClickDelegate ElementClick;

        public Dna Element
        {
            get
            {
                return this.element;
            }

            set
            {
                this.element = value;
                if (this.element != null)
                {
                    this.image.overrideSprite = this.element.Icon;
                }
                else
                {
                    this.image.overrideSprite = null;
                }
            }
        }

        public void OnSelected()
        {
            if (this.Element == null)
            {
                return;
            }

            this.Element.Selected = !this.Element.Selected;
            this.ElementClick?.Invoke(this.Element);
        }

        private void Update()
        {
            if (this.Element == null)
            {
                return;
            }

            if (this.Element.Selected)
            {
                this.image.CrossFadeColor(new Color(1f, 1f, 1f, 0.5f), 0.5f, false, true, false);
            }
            else
            {
                this.image.CrossFadeColor(new Color(1f, 1f, 1f, 1f), 0.5f, false, true, false);
            }
        }

        private void Awake()
        {
            this.image = this.GetComponent<Image>();
        }
    }
}
