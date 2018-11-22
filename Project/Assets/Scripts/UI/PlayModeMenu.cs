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
                this.changeModeButtonText.text = "Do some gardening";
            }
            else if (CursorManager.Instance.CurrentCursor is DefaultCursor)
            {
                CursorManager.Instance.ChangeCursor<GardeningCursor>();
                this.changeModeButtonText.text = "Enjoy your garden";
            }
        }
    }
}
