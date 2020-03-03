using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo.Visualization
{
    public class ColoredCells : MonoBehaviour
    {
        public int Dimension = 50;
        public Transform CellPrefab;

        private Cell[,] _cells;
        private Transform _transform;

        private float _cellSize;
        private float _cellPartSize;

        private void Awake()
        {
            _transform = transform; 
        }

        private void Start()
        {
            GenerateGrid();
        }

        private void Update()
        {
            if (MessageBuss.Input.GetTouch())
            {
                var point = MessageBuss.Input.GetTouchPosition();

                int ix = (int)(point.x / _cellPartSize);
                var iy = (int)(point.y / _cellPartSize);

                //Debug.Log($"{point} / {_cellPartSize} => {ix},{iy}");

                _cells[ix, iy].SetColor(Color.red);
            }
        }

        public void GenerateGrid()
        {
            _cellPartSize = 1f / Dimension;
            _cells = new Cell[Dimension, Dimension];

            var size = Mathf.Min(MessageBuss.Screen.GetWorldHeight(), MessageBuss.Screen.GetWorldWidth());
            var halfSize = 0.5f * size;

            _cellSize = size / Dimension;
            var cellScale = new Vector3(_cellSize, _cellSize, _cellSize);

            var bottomLeft = new Vector2(-0.5f * (size - _cellSize), -0.5f * (size - _cellSize));

            for (int ix = 0; ix < Dimension; ix++)
                for (int iy = 0; iy < Dimension; iy++)
                {
                    var position = bottomLeft + new Vector2(ix * _cellSize, iy * _cellSize);
                    var cellTransform = Instantiate<Transform>(CellPrefab, position, Quaternion.identity);

                    cellTransform.SetParent(_transform);
                    cellTransform.localScale = cellScale;

                    var cell = cellTransform.GetComponent<Cell>();
                    _cells[ix, iy] = cell;
                }
        }

    }
}


