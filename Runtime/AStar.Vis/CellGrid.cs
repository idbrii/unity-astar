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
    public class CellGrid : MonoBehaviour
    {
        public CellPosition _CellPrefab;
        internal List<CellPosition> _Cells = new List<CellPosition>();

        public bool _CreateRandomGrid = true;
        [Range(2,100)]
        public int _NumColumns = 10;
        [Range(2,100)]
        public int _NumRows = 10;
        public Vector2 _Spacing = Vector2.one * 2f;

        byte[,] _Grid;

        PathFinder _Pather;

        void Awake()
        {
            if (_CreateRandomGrid)
            {
                SetRandomGrid();
            }
        }

        void OnEnable()
        {
            // On script reload, we lose our grid and pather (they don't
            // serialize, so randomize a new one).
            if (_Pather == null)
            {
                SetRandomGrid();
            }
        }

        [NaughtyAttributes.Button]
        public void SetRandomGrid()
        {
            var grid = new byte[_NumColumns,_NumRows];
            for (int x = 0; x < grid.GetLength(0); ++x)
            {
                for (int y = 0; y < grid.GetLength(1); ++y)
                {
                    byte v = 1;
                    var r = Random.value;
                    if (r < 0.25f)
                    {
                        // clear
                        v = 1;
                    }
                    else if (r < 0.5f)
                    {
                        // impassable
                        v = 0;
                    }
                    else
                    {
                        v = (byte)Random.Range(2, byte.MaxValue);
                    }
                    grid[x,y] = v;
                }
            }
            SetGrid(grid);
        }

        public void SetGrid(byte[,] grid)
        {
            _Grid = grid;
            _NumColumns = grid.GetLength(0);
            _NumRows = grid.GetLength(1);
            Debug.Log($"Creating path for grid[{_Grid.GetLength(0)},{_Grid.GetLength(0)}]", this);
            var opts = new PathFinderOptions{
                Diagonals = false,
            };
            _Pather = new PathFinder(_Grid, opts);
            CreateVisualGrid(grid);
        }

        void CreateVisualGrid(byte[,] grid)
        {
            for (int i = _Cells.Count; i < _NumColumns * _NumRows; ++i)
            {
                var cell = Instantiate(_CellPrefab, transform);
                _Cells.Add(cell);
            }

            var impedence = Color.cyan;
            for (int x = 0; x < _NumColumns; ++x)
            {
                for (int y = 0; y < _NumRows; ++y)
                {
                    var cell = GetCell(x,y);
                    cell._Pos = new Vector2Int(x, y);
                    var t = cell.transform;
                    t.position = Vector2.Scale(_Spacing, new Vector2(x, y));
                    cell.SetCost(grid[x,y]);

                    cell.gameObject.SetActive(true);
                }
            }
            Debug.Assert((_NumColumns * _NumRows) == _Cells.Count, "Expected a rectangular grid");
        }

        static Point ToPoint(Vector2Int v)
        {
            return new Point(v.x, v.y);
        }

        public void HighlightPath(CellPosition start, CellPosition end)
        {
            var path = _Pather.FindPath(ToPoint(start._Pos), ToPoint(end._Pos));
            if (path == null)
            {
                end.ShowError();
            }
            else
            {
                Debug.Log($"[Pathfind] Found path from {start._Pos} to {end._Pos}:", this);
                var start_color = start._Visual.color;
                var end_color = end._Visual.color;
                var steps = path.Count;
                float i = 1f;
                // For some reason path is returned from end to start.
                path.Reverse();
                foreach (var node in path)
                {
                    Debug.Log($"[Pathfind] {node.X}, {node.Y}", this);
                    var c = GetCell(node.X, node.Y);
                    c._Visual.color = Color.Lerp(start_color, end_color, i/steps);
                    ++i;
                }
            }
        }

        public CellPosition GetCell(int x, int y)
        {
            return _Cells[y + (x * _NumRows)];
        }
        
        public CellPosition FindClosestToPoint(Vector3 world_pos)
        {
            return _Cells
                .GroupBy(item => item, item => (item.transform.position - world_pos).sqrMagnitude)
                .Aggregate((best, next) => {
                    if (next.Last() < best.Last())
                    {
                        return next;
                    }
                    return best;
                })
            .Key;
        }

    }
}
