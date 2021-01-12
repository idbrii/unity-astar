using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine.InputSystem;
using UnityEngine;

using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace AStar.Visualize
{
    /// Draw a path between two ICellVisuals. This component helps to make an
    /// easy demo, but your game probably has a better way of selecting paths.
    class DrawPathOnClick : MonoBehaviour
    {
        GridVisualizer _Grid;
        ExampleGrid _GridInWorld;

        Camera _MainCam;

        ICellVisual _Start;
        ICellVisual _End;

        PathFinder _Pather;

        bool _IsDirty;

        void Awake()
        {
            _GridInWorld = GetComponent<ExampleGrid>();
            _Grid = GetComponent<GridVisualizer>();
        }

        void Start()
        {
            _MainCam = Camera.main;
        }

        bool IsInsideWindow(Vector2 position)
        {
            return (0 < position.x && position.x < Screen.width
                    && 0 < position.y && position.y < Screen.height);
        }

        void OnGUI()
        {
            GUILayout.Label("Left click to set destination.");
            GUILayout.Label("Right click to set start point.");
        }

        void Update()
        {
            var pos = Mouse.current.position.ReadValue();
            if (!IsInsideWindow(pos))
            {
                return;
            }

            if (_Start == null)
            {
                _Start = _Grid.GetCell(0,0);
                _Start.ShowPath(0f);
            }

            if (Mouse.current.rightButton.isPressed)
            {
                SetToItemUnderMouse(pos, ref _Start);
                _Start.ShowPath(0f);
            }
            if (Mouse.current.leftButton.isPressed)
            {
                SetToItemUnderMouse(pos, ref _End);
                _Start.ShowPath(1f);
            }

            if (_IsDirty
                    && _Start != null
                    && _End != null)
            {
                _IsDirty = false;
                NavigateBetween(_Start, _End);
            }
        }

        void SetToItemUnderMouse(Vector2 pos, ref ICellVisual cell)
        {
            var world_pos = _MainCam.ScreenToWorldPoint(pos);
            if (cell != null)
            {
                cell.ShowCost();
            }
            cell = _Grid.FindClosestToPoint(world_pos);
            _IsDirty = true;
        }

        void NavigateBetween(ICellVisual start, ICellVisual destination)
        {
            if (_Pather == null)
            {
                var grid = _GridInWorld._Grid;
                Debug.Log($"Creating path for grid[{grid.GetLength(0)},{grid.GetLength(0)}]", this);
                var opts = new PathFinderOptions{
                    Diagonals = false,
                };
                _Pather = new PathFinder(grid, opts);
            }
            var start_pt = _Grid.GetPoint(start);
            var end_pt = _Grid.GetPoint(destination);
            //
            // Here's the important part:
            // use PathFinder, pass result to HighlightPath.
            var path = _Pather.FindPath(start_pt, end_pt);
            _Grid.HighlightPath(start_pt, end_pt, path);
            //
        }

    }
}
