using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine;

using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace AStar.Visualize
{
    /// An ICellVisual for a SpriteRenderer (2D world-space object).
    public class CellSprite : MonoBehaviour, ICellVisual
    {
        public SpriteRenderer _Visual;

        byte _TraversalCost;

        public byte Cost
        {
            get {
                return _TraversalCost;
            }
            set {
                _TraversalCost = value;
                ShowCost();
            }
        }

        public void ShowCost()
        {
            _Visual.color = GetColorForCost(_TraversalCost);
        }

        public static Color GetColorForCost(byte cost)
        {
            if (cost == 0)
            {
                // impassable
                return Color.black;
            }
            else if (cost == 1)
            {
                // unimpeded
                return Color.white;
            }
            else
            {
                float val = cost / (float)byte.MaxValue;
                return Color.Lerp(Color.white, Color.red, val * 0.5f);
            }
        }

        public void ShowError()
        {
            _Visual.color = Color.red;
        }

        public void ShowPath(float progress)
        {
            _Visual.color = Color.Lerp(Color.green, Color.blue, progress);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

    }
}
