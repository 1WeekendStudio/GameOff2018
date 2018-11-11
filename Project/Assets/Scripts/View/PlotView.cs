namespace View
{
    using System.Collections;
    using UnityEngine;

    public class PlotView : MonoBehaviour
    {
        public Plot Plot;

        public Vector2 VisualSize;

        public void Initialize()
        {
            this.transform.localScale = new Vector3(this.VisualSize.x / 10f, 1f, this.VisualSize.y / 10f);
        }

        public void Update()
        {
        }
    }
}
