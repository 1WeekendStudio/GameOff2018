namespace UI
{
    using Data;

    using UnityEngine;
    using UnityEngine.UI;

    using View;

    public class PlayModeMenu : MonoBehaviour
    {
        [SerializeField]
        private Text changeModeButtonText;
        
        public void ChangeModeCallback()
        {
            if (CursorManager.Instance.CurrentCursor is GardeningCursor)
            {
                CursorManager.Instance.ChangeCursor<DefaultCursor>();
            }
            else if (CursorManager.Instance.CurrentCursor is DefaultCursor)
            {
                CursorManager.Instance.ChangeCursor<GardeningCursor>();
            }
        }

        private void Update()
        {
            if (CursorManager.Instance.CurrentCursor is GardeningCursor)
            {
                this.changeModeButtonText.text = "Do some gardening";
            }
            else if (CursorManager.Instance.CurrentCursor is DefaultCursor)
            {
                this.changeModeButtonText.text = "Enjoy your garden";
            }
        }
    }
}
