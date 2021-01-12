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
    /// Generate a grid for demonstrating pathfinding.
    ///
    /// It's unlikely this class is useful as-is in your game. It's just for
    /// demonstration, but may be a good starting point for creating your
    /// visual grid.
    class ExampleGrid : MonoBehaviour
    {
        public CellSprite _CellPrefab;

        [Tooltip("Cells listed row first: 0,0 1,0 2,0, etc")]
        internal List<CellSprite> _Cells = new List<CellSprite>();

        private GridVisualizer _Visualizer;

        internal byte[,] _Grid;

        [Range(2,100)]
        public int _NumColumns = 10;
        [Range(2,100)]
        public int _NumRows = 10;
        public Vector2 _Spacing = Vector2.one * 2f;

        // Script reload will clear out a bunch of this, so do full init.
        void OnEnable()
        {
            _Visualizer = GetComponent<GridVisualizer>();

            _Grid = CreateRandomGrid();
            CreateVisualForGrid(_Grid);

            var cells = _Cells
                .Select(c => c as ICellVisual)
                .ToList();
            _Visualizer.SetCells(cells, _NumColumns, _NumRows);
        }


        public byte[,] CreateRandomGrid()
        {
            var grid = new byte[_NumColumns,_NumRows];
            for (int y = 0; y < grid.GetLength(1); ++y)
            {
                for (int x = 0; x < grid.GetLength(0); ++x)
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
            return grid;
        }

        void CreateVisualForGrid(byte[,] grid)
        {
            for (int k = _Cells.Count; k < _NumColumns * _NumRows; ++k)
            {
                var cell = Instantiate(_CellPrefab, transform);
                _Cells.Add(cell);
            }

            int i = 0;
            for (int y = 0; y < _NumRows; ++y)
            {
                for (int x = 0; x < _NumColumns; ++x)
                {
                    var cell = _Cells[i] as CellSprite;
                    var t = cell.transform;
                    t.position = Vector2.Scale(_Spacing, new Vector2(x, y));
                    cell.gameObject.SetActive(true);
                    cell.Cost = grid[x,y];
                    ++i;
                }
            }
            Debug.Assert((_NumColumns * _NumRows) == _Cells.Count, "Expected a rectangular grid");
        }

    }
}
