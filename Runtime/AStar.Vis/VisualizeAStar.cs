using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine.InputSystem;
using UnityEngine;

using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace AStar.Editor
{
    public class VisualizeAStar : MonoBehaviour
    {
        CellGrid _Grid;

        Camera _MainCam;

        CellPosition _Start;
        CellPosition _End;

        void Awake()
        {
            _Grid = GetComponent<CellGrid>();
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

        void Update()
        {
            var pos = Mouse.current.position.ReadValue();
            if (!IsInsideWindow(pos))
            {
                return;
            }

            if (Mouse.current.leftButton.isPressed)
            {
                SetToItemUnderMouse(pos, ref _Start);
                _Start.SelectAsStart();
            }
            if (Mouse.current.rightButton.isPressed)
            {
                SetToItemUnderMouse(pos, ref _End);
                _End.SelectAsEnd();
            }

            if (_Start != null && _End != null)
            {
                NavigateBetween(_Start, _End);
            }
        }

        void SetToItemUnderMouse(Vector2 pos, ref CellPosition cell)
        {
            var world_pos = _MainCam.ScreenToWorldPoint(pos);
            if (cell != null)
            {
                cell.ClearSelection();
            }
            cell = _Grid.FindClosestToPoint(world_pos);
        }

        void NavigateBetween(CellPosition start, CellPosition destination)
        {
            foreach (var c in _Grid._Cells)
            {
                c.ClearSelection();
            }
            _Start.SelectAsStart();
            _End.SelectAsEnd();
        }
        
    }
}
