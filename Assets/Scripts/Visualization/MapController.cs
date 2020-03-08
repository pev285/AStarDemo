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

        [Space]
        [SerializeField]
        private Color _pathColor = Color.green;

        [SerializeField]
        private Color _openedNodeColor = Color.grey;
        [SerializeField]
        private Color _closedNodeColor = Color.black;

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

        public MapData GetData()
        {
            return _map;
        }

        public void SetObstacle(Vector2 position)
        {
            var coords = _mapVisualization.GetCoordsByViewPoint(position);

            if (_map.IsValidCell(coords) == false)
                return;

            _map.SetObstacle(coords);
            ColorACell(coords, _obstacleColor);
        }

        public void SetStart(Vector2 position)
        {
            var coords = _mapVisualization.GetCoordsByViewPoint(position);
            SetStartCell(coords);
        }

        public void SetDestination(Vector2 position)
        {
            var coords = _mapVisualization.GetCoordsByViewPoint(position);
            SetDestinationCell(coords);
        }

        public void DrawPath(List<Vector2Int> path)
        {
            var start = path[0];
            var dest = path[path.Count-1];

            foreach (var node in path)
                ColorACell(node, _pathColor);

            ColorACell(start, _startColor);
            ColorACell(dest, _destinationColor);
        }

        public void SetStartCell(Vector2Int coords)
        {
            if (_map.IsValidCell(coords) == false)
                return;

            if (_map.Get(coords) != MapCodes.Empty)
                return;

            if (_map.Start.HasValue)
                ColorACell(_map.Start.Value, _backgroundColor);

            _map.SetStart(coords);
            ColorACell(coords, _startColor);

            MessageBuss.Map.StartPositionChosen(coords);
        }

        public void SetDestinationCell(Vector2Int coords)
        {
            if (_map.IsValidCell(coords) == false)
                return;

            if (_map.Get(coords) != MapCodes.Empty)
                return;

            if (_map.Destination.HasValue)
                ColorACell(_map.Destination.Value, _backgroundColor);

            _map.SetDestination(coords);
            ColorACell(coords, _destinationColor);

            MessageBuss.Map.DestinationPositionChosen(coords);
        }

        public void SetOpenedCell(Vector2Int coords)
        {
            ColorACell(coords, _openedNodeColor);
        }

        public void SetClosedCell(Vector2Int coords)
        {
            ColorACell(coords, _closedNodeColor);
        }

        public void SetPathCell(Vector2Int coords)
        {
            ColorACell(coords, _pathColor);
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
            if (_map.IsValidCell(coords) == false)
                return;

            _mapVisualization.SetCellColor(coords, color);
        }
    }
}

