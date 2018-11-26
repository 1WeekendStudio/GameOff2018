namespace UI
{
    using System.Collections.Generic;
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
                this.image.overrideSprite = this.element.Icon;
            }
        }

        public void OnSelected()
        {
            this.ElementClick?.Invoke(this.Element);
        }

        private void Awake()
        {
            this.image = this.GetComponent<Image>();
        }
    }
}
