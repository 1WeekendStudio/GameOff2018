namespace View
{
    using System;

    using Assets.Scripts;

    using UnityEngine;

    using Random = UnityEngine.Random;

    public class PlotView : MonoBehaviour
    {
        [NonSerialized]
        public Plot Plot;

        [NonSerialized]
        public Vector2 VisualSize;

        [SerializeField]
        private Color[] waterColors;

        [SerializeField]
        private Color[] sunshineColors;

        [SerializeField]
        private Color[] propagationColors;

        private GameObject[,] plantObjects;

        public void Initialize()
        {
            this.transform.localScale = new Vector3(this.VisualSize.x / 10f, 1f, this.VisualSize.y / 10f);
            this.plantObjects = new GameObject[this.Plot.Width, this.Plot.Height];
        }

        public void Update()
        {
            for (int x = 0; x < this.Plot.Width; x++)
            {
                for (int y = 0; y < this.Plot.Height; y++)
                {
                    SoilTile soilTile = this.Plot.Soil[x, y];
                    if (soilTile.Plant == null && this.plantObjects[x, y] != null)
                    {
                        GameObject.Destroy(this.plantObjects[x, y]);
                        this.plantObjects[x, y] = null;
                    }
                    else if (soilTile.Plant != null && this.plantObjects[x, y] == null)
                    {
                        this.plantObjects[x, y] = PlantGenerator.Instance.CreatePlant(soilTile.Plant.Description);
                        this.plantObjects[x, y].transform.position = this.GetTilePosition(new Position(x, y));
                        this.plantObjects[x, y].transform.SetParent(this.transform);
                    }
                    else if (this.plantObjects[x, y] != null)
                    {
                        var component = this.plantObjects[x, y].GetComponent<Animator>();

                        // TODO: Update anim with lifetime.
                    }
                }
            }
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
