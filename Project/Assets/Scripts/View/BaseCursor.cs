namespace View
{
    using UnityEngine;

    public abstract class BaseCursor : ICursor
    {
        private Vector3 origin;
        private Vector3 direction;

        public abstract void OnActivate(object parameter);

        public void Update(Camera camera)
        {
            this.origin = camera.transform.position;
            this.direction = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane)) - this.origin;

            RaycastHit hitInfo;
            if (Physics.Raycast(new Ray(this.origin, this.direction), out hitInfo, 1000, LayerMask.NameToLayer("Ground")))
            {
                PlotView plotView = hitInfo.transform.GetComponent<PlotView>();
                Debug.Assert(plotView != null);

                Position tile = plotView.FindNearestTile(hitInfo.point);

                this.Update(camera, plotView, tile);
            }
            else
            {
                this.Update(camera, null, Position.Invalid);
            }
        }

        public abstract void OnDeactivate();

        public virtual void OnDrawGizmos()
        {
            Gizmos.DrawLine(this.origin, this.origin + (1000 * this.direction));
        }

        protected abstract void Update(Camera camera, PlotView plotView, Position tile);
    }
}
