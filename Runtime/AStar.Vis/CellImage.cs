using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine.UI;
using UnityEngine;

using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace AStar.Visualize
{
    /// An ICellVisual for a UI.Image (2D Canvas object).
    public class CellImage : MonoBehaviour, ICellVisual
    {
        public Image _Visual;

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
            _Visual.color = CellSprite.GetColorForCost(_TraversalCost);
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
