using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine;

using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace AStar.Visualize
{
    public class CellPosition : MonoBehaviour
    {
        public Vector2Int _Pos;
        [Range(0,byte.MaxValue)]
        public byte _TraversalCost;

        public SpriteRenderer _Visual;


        public void SetCost(byte cost)
        {
            _TraversalCost = cost;
            DisplayCost();
        }

        void DisplayCost()
        {
            if (_TraversalCost == 0)
            {
                // impassable
                _Visual.color = Color.black;
            }
            else if (_TraversalCost == 1)
            {
                // unimpeded
                _Visual.color = Color.white;
            }
            else
            {
                float val = _TraversalCost / (float)byte.MaxValue;
                _Visual.color = Color.Lerp(Color.white, Color.red, val * 0.5f);
            }
        }

        public void ClearSelection()
        {
            DisplayCost();
        }

        public void SelectAsStart()
        {
            _Visual.color = Color.green;
        }

        public void SelectAsEnd()
        {
            _Visual.color = Color.blue;
        }

        public void ShowError()
        {
            _Visual.color = Color.red;
        }

#if UNITY_EDITOR
        GUIStyle _DebugText;
        void OnDrawGizmos() //~ OnDrawGizmosSelected()
        {
            if (_DebugText == null)
            {
                _DebugText = new GUIStyle();
                _DebugText.normal.textColor = Color.blue;
                _DebugText.fontStyle = FontStyle.Bold;
            }

            var pos = transform.position;
            UnityEditor.Handles.Label(pos, $"cell({_Pos.x},{_Pos.y})\ncost:{_TraversalCost}", _DebugText);
        }
#endif

    }
}
