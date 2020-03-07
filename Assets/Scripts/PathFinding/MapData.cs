using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo.PathFinding 
{
	public class MapData
	{
		public int Width { get; }

		public int Height { get; }

		public MapCodes[,] Map { get; }

		public Vector2Int? Start { get; private set; }
		public Vector2Int? Destination { get; private set; }

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
			Start = null;
			Destination = null;

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
			if (Start.HasValue)
				ClearCell(Start.Value);

			Start = position;
		}

		public void SetDestination(Vector2Int position)
		{
			if (Destination.HasValue)
				ClearCell(Destination.Value);

			Destination = position;
		}

		public void ClearCell(Vector2Int position)
		{
			Set(position, MapCodes.Empty);
		}

		public void Set(Vector2Int position, MapCodes value)
		{
			Map[position.x, position.y] = value;
		}

		public bool IsValidCell(Vector2Int coords)
		{
			if (0 > coords.x || coords.x >= Width)
				return false;

			if (0 > coords.y || coords.y >= Height)
				return false;

			return true;
		}
	}
} 


