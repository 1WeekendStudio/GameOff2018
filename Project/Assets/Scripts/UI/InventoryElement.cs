namespace UI
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class InventoryElement : MonoBehaviour
    {
        private Image image;

        private Dna element;

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

        private void Awake()
        {
            this.image = this.GetComponent<Image>();
        }
        
        public void OnSelected()
        {
            Debug.Log("Select dna " + this.Element.Name);
        }
    }
}
