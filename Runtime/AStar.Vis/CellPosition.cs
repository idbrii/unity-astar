using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine;

using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace AStar.Editor
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
                var impedence = Color.cyan;
                impedence.r = _TraversalCost / byte.MaxValue;
                _Visual.color = impedence;
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

    }
}
