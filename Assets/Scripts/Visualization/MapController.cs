using AStarDemo.Global;
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

            if (IsValidCell(coords) == false)
                return;

            _map.SetObstacle(coords);
            ColorACell(coords, _obstacleColor);
        }

        public void SetStart(Vector2 position)
        {
            var coords = _mapVisualization.GetCoordsByViewPoint(position);

            if (IsValidCell(coords) == false)
                return;

            if (_map.Start.HasValue)
                ColorACell(_map.Start.Value, _backgroundColor);

            _map.SetStart(coords);
            ColorACell(coords, _startColor);
        }

        public void SetDestination(Vector2 position)
        {
            var coords = _mapVisualization.GetCoordsByViewPoint(position);

            if (IsValidCell(coords) == false)
                return;

            if (_map.Destination.HasValue)
                ColorACell(_map.Destination.Value, _backgroundColor);

            _map.SetDestination(coords);
            ColorACell(coords, _destinationColor);
        }

        //public void ClearCell(Vector2 position)
        //{
        //    var coords = _mapVisualization.GetCoordsByViewPoint(position);

        //    if (IsValidCell(coords) == false)
        //        return;

        //    _map.ClearCell(coords);
        //    ColorACell(coords, _backgroundColor);
        //}

        private void ColorACell(Vector2Int coords, Color color)
        {
            _mapVisualization.SetCellColor(coords, color);
        }

        public bool IsValidCell(Vector2Int coords)
        {
            if (0 > coords.x || coords.x >= _dim)
                return false;

            if (0 > coords.y || coords.y >= _dim)
                return false;

            return true;
        }
    }
}

