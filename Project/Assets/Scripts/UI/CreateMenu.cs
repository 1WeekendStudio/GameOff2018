namespace UI
{
    using Data;

    using UnityEngine;
    using UnityEngine.UI;

    using View;

    public class CreateMenu : MonoBehaviour
    {
        [SerializeField]
        private Button createButton;

        [SerializeField]
        private Text info;

        public void CreatePlantCallback()
        {
            PlantDescription defaultPlant = new PlantDescription();
            CursorManager.Instance.ChangeCursor<PlantPlacementCursor>(defaultPlant);
        }

        private void Start()
        {
        }
        
        private void Update()
        {
            if (CursorManager.Instance.CurrentCursor is PlantPlacementCursor)
            {
                this.createButton.gameObject.SetActive(false);
                this.info.gameObject.SetActive(true);
            }
            else
            {
                this.createButton.gameObject.SetActive(true);
                this.info.gameObject.SetActive(false);
            }
        }
    }
}
