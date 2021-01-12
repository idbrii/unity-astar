using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine;

using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace AStar.Editor
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

        void Awake()
        {
            if (_CreateRandomGrid)
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
                    if (r > 0.75f)
                    {
                        // impassable
                        v = 0;
                    }
                    else if (r > 0.95f)
                    {
                        v = (byte)Random.Range(10, byte.MaxValue);
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
            CreateVisualGrid(grid);
        }

        void CreateVisualGrid(byte[,] grid)
        {
            _Cells.Clear();
            var impedence = Color.cyan;
            for (int x = 0; x < _NumColumns; ++x)
            {
                for (int y = 0; y < _NumRows; ++y)
                {
                    var cell = Instantiate(_CellPrefab, transform);
                    cell._Pos = new Vector2Int(x, y);
                    var t = cell.transform;
                    t.position = Vector2.Scale(_Spacing, new Vector2(x, y));
                    cell.SetCost(grid[x,y]);

                    cell.gameObject.SetActive(true);
                    _Cells.Add(cell);
                }
            }
            Debug.Assert(_NumColumns * _NumRows == _Cells.Count, "Expected a square grid");
        }



        public CellPosition GetCell(int x, int y)
        {
            return _Cells[x + (y * _NumColumns)];
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
