﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SuperTiled2Unity
{
    public class SuperLayer : MonoBehaviour
    {
        [ReadOnly]
        public string m_TiledName;

        [ReadOnly]
        public float m_OffsetX;

        [ReadOnly]
        public float m_OffsetY;

        [ReadOnly]
        public float m_Opacity;

        [ReadOnly]
        public bool m_Visible;

        [ReadOnly]
        public string m_SortingLayerName;

        [ReadOnly]
        public int m_SortingOrder;

        public float CalculateOpacity()
        {
            float opacity = 1.0f;

            foreach (var layer in gameObject.GetComponentsInParent<SuperLayer>())
            {
                opacity *= layer.m_Opacity;
            }

            return opacity;
        }
    }
}
