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
        private Color _obstacleColor;
        [SerializeField]
        private Color _backgroundColor;

        [SerializeField]
        private Color _startColor;
        [SerializeField]
        private Color _destinationColor;

        private ColoredCells _map;

        private void Awake()
        {
            _map = GetComponent<ColoredCells>();
        }

        private void Start()
        {
            _map.GenerateGrid();
            _map.Fill(_backgroundColor);
        }

        //--- Temporary ---
        private void Update()
        {
            if (MessageBuss.Input.GetTouch())
            {
                var point = MessageBuss.Input.GetTouchPosition();
                var coords = _map.GetCoordsByViewPoint(point);

                _map.SetCellColor(coords, _obstacleColor);
            }
        }
        //--------------------------

        public IMapData GetData()
        {
            throw new NotImplementedException();
        }

        public void SetObstacle(Vector2 position)
        {
            ColorACell(position, _obstacleColor);
        }

        public void SetStart(Vector2 position)
        {
            ColorACell(position, _startColor);
        }

        public void SetDestination(Vector2 position)
        {
            ColorACell(position, _destinationColor);
        }

        public void ClearCell(Vector2 position)
        {
            ColorACell(position, _backgroundColor);
        }

        private void ColorACell(Vector2 position, Color color)
        {
            var coords = _map.GetCoordsByViewPoint(position);
            _map.SetCellColor(coords, color);
        }
    }
}

