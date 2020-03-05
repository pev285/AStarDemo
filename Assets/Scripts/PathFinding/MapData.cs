using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo.PathFinding 
{
	public class MapData : IMapData
	{
		public int Width { get; }

		public int Height { get; }

		public Vector2Int Start 
		{ 
			get
			{
				if (_start.HasValue)
					return _start.Value;

				return Vector2Int.zero;
			}
		}

		public Vector2Int Destination
		{
			get
			{
				if (_destination.HasValue)
					return _destination.Value;

				return Vector2Int.zero;
			}
		}

		public MapCodes[,] Map { get; }


		private Vector2Int? _start;
		private Vector2Int? _destination;

		public MapCodes Get(int x, int y)
		{
			return Map[x, y];
		}

		public MapCodes Get(Vector2Int cell)
		{
			return Get(cell.x, cell.y);
		}

		public MapData(int width, int height)
		{
			Width = width;
			Height = height;

			Map = new MapCodes[Width, Height];
			Clear();
		}

		public void Clear()
		{
			_start = null;
			_destination = null;

			for (int ix = 0; ix < Width; ix++)
				for (int iy = 0; iy < Height; iy++)
					Map[ix, iy] = MapCodes.Empty;
		}


		public void SetObstacle(Vector2Int position)
		{
			Map[position.x, position.y] = MapCodes.Obstacle;
		}

		public void SetStart(Vector2Int position)
		{
			if (_start.HasValue)
				ClearCell(_start.Value);

			_start = position;
			Set(position, MapCodes.Start);
		}

		public void SetDestination(Vector2Int position)
		{
			if (_destination.HasValue)
				ClearCell(_destination.Value);

			_destination = position;
			Set(position, MapCodes.Destination);
		}

		public void ClearCell(Vector2Int position)
		{
			Set(position, MapCodes.Empty);
		}

		private void Set(Vector2Int position, MapCodes value)
		{
			Map[position.x, position.y] = value;
		}
	}
} 


