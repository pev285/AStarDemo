using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo.PathFinding
{
	public class SimpleMapData : IMapData
	{
		public int Width => throw new NotImplementedException();

		public int Height => throw new NotImplementedException();

		public Vector2Int Start => throw new NotImplementedException();

		public Vector2Int Destination => throw new NotImplementedException();

		public MapCodes[,] Map => throw new NotImplementedException();

		public MapCodes GetValueIn(int x, int y)
		{
			throw new NotImplementedException();
		}

		public MapCodes GetValueIn(Vector2Int cell)
		{
			throw new NotImplementedException();
		}
	}
} 


