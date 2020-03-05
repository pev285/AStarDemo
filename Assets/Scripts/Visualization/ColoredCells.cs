using AStarDemo.Global;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo.Visualization
{
    public class ColoredCells : MonoBehaviour
    {
        public int Dim = 50;

        public Transform CellPrefab;
        public float CellScale = 0.9f;

        private Cell[,] _cells;
        private Transform _transform;

        private float _viewCellSize;
        private float _worldCellSize;

        private void Awake()
        {
            _transform = transform; 
        }


        public Vector2Int GetCoordsByViewPoint(Vector2 point)
        {
            var ix = (int)(point.x / _viewCellSize);
            var iy = (int)(point.y / _viewCellSize);

            return new Vector2Int(ix, iy);
        }

        public void SetCellColor(Vector2Int pos, Color color)
        {
            SetCellColor(pos.x, pos.y, color);
        }

        public void SetCellColor(int ix, int iy, Color color)
        {
            if (ix < 0 || ix >= Dim)
                return;

            if (iy < 0 || iy >= Dim)
                return;

            _cells[ix, iy].SetColor(color);
        }

        public void Fill(Color color)
        {
            for (int ix = 0; ix < Dim; ix++)
                for (int iy = 0; iy < Dim; iy++)
                    SetCellColor(ix, iy, color);
        }

        public void Create()
        {
            _viewCellSize = 1f / Dim;
            _cells = new Cell[Dim, Dim];

            var size = Mathf.Min(MessageBuss.Screen.GetWorldHeight(), MessageBuss.Screen.GetWorldWidth());
            var halfSize = 0.5f * size;

            _worldCellSize = size / Dim;
            var cellScale = _worldCellSize * CellScale * Vector3.one;

            var bottomLeft = new Vector2(-0.5f * (size - _worldCellSize), -0.5f * (size - _worldCellSize));

            for (int ix = 0; ix < Dim; ix++)
                for (int iy = 0; iy < Dim; iy++)
                {
                    var position = bottomLeft + new Vector2(ix * _worldCellSize, iy * _worldCellSize);
                    var cellTransform = Instantiate<Transform>(CellPrefab, position, Quaternion.identity);

                    cellTransform.SetParent(_transform);
                    cellTransform.localScale = cellScale;

                    var cell = cellTransform.GetComponent<Cell>();
                    _cells[ix, iy] = cell;
                }
        }

        public void Remove()
        {
            if (_cells == null)
                return;

            for (int ix = 0; ix < Dim; ix++)
                for (int iy = 0; iy < Dim; iy++)
                    Destroy(_cells[ix, iy].gameObject);

            _cells = null;
        }
    }
}


