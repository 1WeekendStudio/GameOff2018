namespace View
{
    using System.Collections;
    using UnityEngine;

    public class PlotView : MonoBehaviour
    {
        public Plot Plot;

        public Vector2 VisualSize;
        private Texture2D gridState;

        public void Initialize()
        {
            this.transform.localScale = new Vector3(this.VisualSize.x / 10f, 1f, this.VisualSize.y / 10f);

            this.gridState = new Texture2D(this.Plot.Width, this.Plot.Height);
            for (int x = 0; x < this.Plot.Width; x++)
            {
                for (int y = 0; y < this.Plot.Height; y++)
                {
                    this.gridState.SetPixel(x, y, Color.clear);
                }
            }

            this.gridState.Apply();

            MeshRenderer[] meshRenderers = this.GetComponentsInChildren<MeshRenderer>();

            Material gridMaterial = null;
            for (int index = 0; index < meshRenderers.Length; index++)
            {
                if (meshRenderers[index].gameObject.name == "Grid")
                {
                    gridMaterial = meshRenderers[index].material;
                    break;
                }
            }

            gridMaterial.SetInt("_GridWidth", this.Plot.Width);
            gridMaterial.SetInt("_GridHeight", this.Plot.Height);
            gridMaterial.SetTexture("_TileStatesTexture", this.gridState);
        }

        public void Update()
        {
        }
    }
}
