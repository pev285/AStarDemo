using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo.PathFinding
{
	public class SimpleAStar : IPathFinder
	{
		private const int DiagCost = 14;
		private const int StraightCost = 10;

		public event Action<bool> SearchCompleted = (a) => { };
		public event Action<Vector2Int> PathNodeFound = (n) => { };

		public event Action<Vector2Int> NodeOpened = (n) => { };
		public event Action<Vector2Int> NodeClosed = (n) => { };

		public bool IsInProgress { get; private set; } = false;
		private List<Vector2Int> _frontLine = new List<Vector2Int>();

		private MapData _mapData;
		private int[,] _calculatedPaths;

		private Vector2Int[] _straightNeighbs =
		{
			new Vector2Int(0, 1),
			new Vector2Int(1, 0),
			new Vector2Int(0, -1),
			new Vector2Int(-1, 0),
		};

		private Vector2Int[] _diagNeighbs =
		{
			new Vector2Int(1, 1),
			new Vector2Int(1, -1),
			new Vector2Int(-1, 1),
			new Vector2Int(-1, -1),
		};

		private List<Vector2Int> _foundPath = new List<Vector2Int>();

		public List<Vector2Int> GetResults()
		{
			return _foundPath;
		}

		public void Stop()
		{
			IsInProgress = false;
		}

		public void Start(MapData data)
		{
			_mapData = data;
			_frontLine.Clear();

			var start = _mapData.Start.Value;

			ResetPaths(start);
			_frontLine.Add(start);

			//var go = new GameObject().GetComponent<Transform>();
			//go.StartCoroutine()
			CalculationProcess();
		}

		private void ResetPaths(Vector2Int start)
		{
			_calculatedPaths = new int[_mapData.Width, _mapData.Height];

			for (int x = 0; x < _mapData.Width; x++)
				for (int y = 0; y < _mapData.Height; y++)
					_calculatedPaths[x, y] = int.MaxValue;
		}

		private void CalculationProcess()
		{
			IsInProgress = true;
			bool success = MainCycle();

			if (success)
				BackPassFoundPath();

			IsInProgress = false;
			SearchCompleted(success);
		}

		private void BackPassFoundPath()
		{
			var start = _mapData.Start.Value;
			var current = _mapData.Destination.Value;
			
			_foundPath.Add(current);
			PathNodeFound.Invoke(current);

			while (current != start)
			{
				current = FindPreviousPathNode(current);

				_foundPath.Add(current);
				PathNodeFound.Invoke(current);
			}

			_foundPath.Reverse();
		}

		private Vector2Int FindPreviousPathNode(Vector2Int point)
		{
			var pointCost = _calculatedPaths[point.x, point.y];

			foreach (var shift in _straightNeighbs)
			{
				var stepCost = StraightCost;
				var neighbor = point + shift;
			
				if (IsPrevious(neighbor, pointCost, stepCost))
					return neighbor;
			}

			foreach (var shift in _diagNeighbs)
			{
				var stepCost = DiagCost;
				var neighbor = point + shift;

				if (IsPrevious(neighbor, pointCost, stepCost))
					return neighbor;
			}

			throw new Exception("Unexpected situation: previous path point wasn't found.");
		}

		private bool IsPrevious(Vector2Int neighbor, int pointCost, int stepCost)
		{
			if (_mapData.IsValidCell(neighbor) == false)
				return false;

			var nodeMark = _mapData.Get(neighbor);
			if (nodeMark != MapCodes.Closed)
				return false;

			if (_calculatedPaths[neighbor.x, neighbor.y] + stepCost == pointCost)
				return true;

			return false;
		}

		private bool MainCycle()
		{
			while (_frontLine.Count > 0)
			{
				var node = GetBestNode();
				_frontLine.Remove(node);

				if (_mapData.Destination.Value == node)
					return true;

				UpdateNeighbors(node);
				_mapData.Set(node, MapCodes.Closed);

				NodeClosed.Invoke(node);
			}

			return false;
		}

		private Vector2Int GetBestNode()
		{
			var bestNode = _frontLine[0];
			var bestCost = EvaluateNode(bestNode);

			foreach (var node in _frontLine)
			{
				var nodeCost = EvaluateNode(node);

				if (nodeCost >= bestCost)
					continue;

				bestNode = node;
				bestCost = nodeCost;
			}

			return bestNode;
		}

		private void UpdateNeighbors(Vector2Int point)
		{
			foreach (var shift in _straightNeighbs)
				UpdateNeighbor(point, shift, StraightCost);

			foreach (var shift in _diagNeighbs)
				UpdateNeighbor(point, shift, DiagCost);
		}

		private void UpdateNeighbor(Vector2Int point, Vector2Int shift, int stepCost)
		{
			var neighbor = point + shift;
			if (_mapData.IsValidCell(neighbor) == false)
				return;

			var nodeMark = _mapData.Get(neighbor);
			if (nodeMark == MapCodes.Closed || nodeMark == MapCodes.Obstacle)
				return;

			if (nodeMark == MapCodes.Empty)
			{
				_frontLine.Add(neighbor);
				_mapData.Set(neighbor, MapCodes.Open);

				NodeOpened.Invoke(neighbor);
			}

			var newCost = _calculatedPaths[point.x, point.y] + stepCost;
			var oldCost = _calculatedPaths[neighbor.x, neighbor.y];

			if (newCost < oldCost)
				_calculatedPaths[neighbor.x, neighbor.y] = newCost;
		}

		private int EvaluateNode(Vector2Int point)
		{
			var knownPath = _calculatedPaths[point.x, point.y];
			var estimation = EstimateDistance(point, _mapData.Destination.Value);

			return knownPath + estimation;
		}

		private static int EstimateDistance(Vector2Int a, Vector2Int b)
		{
			var delta = a - b;
			var dx = Mathf.Abs(delta.x);
			var dy = Mathf.Abs(delta.y);

			var min = Mathf.Min(dx, dy);
			var max = Mathf.Max(dx, dy);

			return 14 * min + 10 * (max - min);
		}
	}
} 


