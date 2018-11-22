using System;
using System.Collections.Generic;

using UnityEngine;

using View;

public class CursorManager : MonoBehaviour
{
    [NonSerialized]
    public PlotView HoveredPlot = null;
    [NonSerialized]
    public Position HoveredPlotPosition = Position.Invalid;
    [NonSerialized]
    public PlotView SelectedPlot = null;
    [NonSerialized]
    public Position SelectedPlotPosition = Position.Invalid;

    [SerializeField]
    private GameObject hoverPrefab;

    [SerializeField]
    private GameObject selectionPrefab;

    private Dictionary<System.Type, ICursor> cursors = new Dictionary<Type, ICursor>();
    private ICursor currentCursor;

    private Camera currentCamera = null;

    public static CursorManager Instance { get; private set; }

    public ICursor CurrentCursor => this.currentCursor;

    public void ChangeCursor<T>(object parameter = null) where T : ICursor
    {
        this.currentCursor?.OnDeactivate();

        if (!this.cursors.TryGetValue(typeof(T), out this.currentCursor))
        {
            Debug.LogError($"Unknown cursor: {typeof(T)}");
            return;
        }

        this.currentCursor.OnActivate(parameter);
    }

    private void Awake()
    {
        Debug.Assert(CursorManager.Instance == null);
        CursorManager.Instance = this;

        var plantPlacementCursor = new GardeningCursor();
        plantPlacementCursor.LoadPrefabs(this.hoverPrefab, this.selectionPrefab);

        this.cursors.Add(typeof(GardeningCursor), plantPlacementCursor);
        this.cursors.Add(typeof(DefaultCursor), new DefaultCursor());
    }

    private void Update()
    {
        if (this.currentCamera == null)
        {
            this.currentCamera = GameObject.FindObjectOfType<Camera>();
            return;
        }

        this.currentCursor?.Update(this.currentCamera);
    }

    private void OnDrawGizmos()
    {
        this.currentCursor?.OnDrawGizmos();
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        if (this.currentCursor != null)
        {
            GUILayout.Space(250f);
            GUILayout.Label($"Cursor {this.currentCursor.GetType().Name}");
            if (SelectedPlot != null)
            {
                GUILayout.Label($"SelectedPlot: true");
            }
            else
            {
                GUILayout.Label($"SelectedPlot: false");
            }
            GUILayout.Label($"Selected position: {this.SelectedPlotPosition}");
        }
    }
#endif
}