using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotGizmo : MonoBehaviour
{
    [SerializeField]
    public Data.PlotDescription PlotDescription;

    [SerializeField]
    public int GameplayWidth;

    [SerializeField]
    public int GameplayHeight;

    [SerializeField]
    public Vector2 VisualSize;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.43f, 0.36f, 0.31f);
        Gizmos.matrix = Matrix4x4.Translate(this.transform.position) * Matrix4x4.Rotate(this.transform.localRotation);
        Gizmos.DrawCube(Vector3.zero, new Vector3(this.VisualSize.x, 0.1f, this.VisualSize.y));
    }
}
