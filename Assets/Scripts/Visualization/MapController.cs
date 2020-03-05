﻿using AStarDemo.Global;
using AStarDemo.PathFinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo.Visualization
{
    [RequireComponent(typeof(ColoredCells))]
    public class MapController : MonoBehaviour
    {


        [SerializeField]
        private Color _obstacleColor = Color.red;
        [SerializeField]
        private Color _backgroundColor = Color.gray;

        [SerializeField]
        private Color _startColor = Color.blue;
        [SerializeField]
        private Color _destinationColor = Color.magenta;

        [SerializeField]
        private Color _pathColor = Color.green;

        [Space]
        [SerializeField]
        private int _dim = 50;

        private MapData _map;
        private ColoredCells _mapVisualization;

        private void Awake()
        {
            _map = new MapData(_dim, _dim);
            _mapVisualization = GetComponent<ColoredCells>();
        }

        private void Start()
        {
            Clear();
        }

        //--- Temporary ---
        private void Update()
        {
            if (MessageBuss.Input.GetTouch())
            {
                var point = MessageBuss.Input.GetTouchPosition();
                var coords = _mapVisualization.GetCoordsByViewPoint(point);

                _mapVisualization.SetCellColor(coords, _obstacleColor);
            }
        }
        //--------------------------

        public void Clear()
        {
            _map.Clear();

            _mapVisualization.Remove();
            _mapVisualization.Dim = _dim;

            _mapVisualization.Create();
            _mapVisualization.Fill(_backgroundColor);
        }

        public IMapData GetData()
        {
            return _map;
        }

        public void SetObstacle(Vector2 position)
        {
            var coords = _mapVisualization.GetCoordsByViewPoint(position);
            _map.SetObstacle(coords);

            ColorACell(position, _obstacleColor);
        }

        public void SetStart(Vector2 position)
        {
            var coords = _mapVisualization.GetCoordsByViewPoint(position);
            _map.SetStart(coords);

            ColorACell(position, _startColor);
        }

        public void SetDestination(Vector2 position)
        {
            var coords = _mapVisualization.GetCoordsByViewPoint(position);
            _map.SetDestination(coords);

            ColorACell(position, _destinationColor);
        }

        public void ClearCell(Vector2 position)
        {
            var coords = _mapVisualization.GetCoordsByViewPoint(position);
            _map.ClearCell(coords);

            ColorACell(position, _backgroundColor);
        }

        private void ColorACell(Vector2 position, Color color)
        {
            var coords = _mapVisualization.GetCoordsByViewPoint(position);
            _mapVisualization.SetCellColor(coords, color);
        }
    }
}

