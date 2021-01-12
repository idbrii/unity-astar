using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine;

using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace AStar.Visualize
{
    public interface ICellVisual
    {
        byte Cost { get; set; }
        void ShowCost();
        void ShowError();
        // progress is in [0,1]
        void ShowPath(float progress);
        Vector3 GetPosition();
    }

    /// Call SetCells and then you can call HighlightPath to visualize the path
    /// on the input cells.
    public class GridVisualizer : MonoBehaviour
    {
        List<ICellVisual> _Cells;

        internal int _NumColumns = 10;
        internal int _NumRows = 10;

        // Cells listed row first: 0,0 1,0 2,0, etc
        public void SetCells(List<ICellVisual> cells, int columns, int rows)
        {
            _Cells = cells;
            _NumColumns = columns;
            _NumRows = rows;
        }

        public void HighlightPath(Point start, Point end, List<PathFinderNode> path)
        {
            foreach (var c in _Cells)
            {
                c.ShowCost();
            }
            if (path == null)
            {
                var start_vis = GetCell(start.X, start.Y);
                start_vis.ShowPath(0f);
                var end_vis = GetCell(end.X, end.Y);
                end_vis.ShowError();
            }
            else
            {
                Debug.Log($"[Pathfind] Found path from {start} to {end}:", this);
                var steps = path.Count;
                float i = 1f;
                // For some reason path is returned from end to start.
                path.Reverse();
                foreach (var node in path)
                {
                    Debug.Log($"[Pathfind]     {node.X}, {node.Y}", this);
                    var c = GetCell(node.X, node.Y);
                    c.ShowPath(i/steps);
                    ++i;
                }
            }
        }

        public ICellVisual GetCell(int x, int y)
        {
            return _Cells[x + (y * _NumColumns)];
        }


        public Point GetPoint(ICellVisual c)
        {
            int pos = _Cells.IndexOf(c);
            int x = pos % _NumColumns;
            int y = pos / _NumColumns;
            return new Point(x,y);
        }
        
        public ICellVisual FindClosestToPoint(Vector3 world_pos)
        {
            return _Cells
                .GroupBy(item => item, item => ((item as Behaviour).transform.position - world_pos).sqrMagnitude)
                .Aggregate((best, next) => {
                    if (next.Last() < best.Last())
                    {
                        return next;
                    }
                    return best;
                })
            .Key;
        }

#if UNITY_EDITOR
        GUIStyle _DebugText;
        void OnDrawGizmosSelected() //~ OnDrawGizmos()
        {
            if (_Cells == null)
            {
                return;
            }

            if (_DebugText == null)
            {
                _DebugText = new GUIStyle();
                _DebugText.normal.textColor = Color.blue;
                _DebugText.fontStyle = FontStyle.Bold;
            }

            foreach (var c in _Cells)
            {
                var pos = c.GetPosition();
                UnityEditor.Handles.Label(pos, $"cell({pos.x},{pos.y})\ncost:{c.Cost}", _DebugText);
            }
        }
#endif

    }
}
