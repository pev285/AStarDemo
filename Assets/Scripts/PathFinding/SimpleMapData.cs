using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo.PathFinding
{
	public class SimpleMapData : IMapData
	{
		public int Width { get; }
		public int Height { get; }

		public float GetStepPrice(Vector2Int start, Vector2Int destination)
		{
			throw new NotImplementedException();
		}

		public float GetEstimation(Vector2Int start, Vector2Int destination)
		{
			throw new NotImplementedException();
		}

	} 
} 


