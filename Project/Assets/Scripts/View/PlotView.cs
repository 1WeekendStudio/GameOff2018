namespace View
{
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

        public Position FindNearestTile(Vector3 hitInfoPoint)
        {
            Vector3 relativePositionToCenter = Quaternion.Inverse(this.transform.rotation) * (hitInfoPoint - this.transform.position);
            Vector2 relativePosition = (this.VisualSize / 2f) + new Vector2(relativePositionToCenter.x, relativePositionToCenter.z);
            Vector2 ratio = relativePosition / this.VisualSize;
            return new Position(Mathf.FloorToInt(ratio.x * this.Plot.Width), Mathf.FloorToInt(ratio.y * this.Plot.Height));
        }

        public Vector3 GetTilePosition(Position tile)
        {
            Vector2 size = new Vector2(this.Plot.Width, this.Plot.Height);
            Vector2 ratio = (Vector2)tile / size;
            Vector2 relativePosition = ratio * this.VisualSize;
            Vector2 offset = (this.VisualSize / size) / 2f;
            Vector2 relativePositionToCenter = offset + relativePosition - (this.VisualSize / 2f);
            return this.transform.position + (this.transform.rotation * new Vector3(relativePositionToCenter.x, 0f, relativePositionToCenter.y));
        }
    }
}
