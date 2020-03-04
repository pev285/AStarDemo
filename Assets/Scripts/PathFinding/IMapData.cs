using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo.PathFinding
{
	public interface IMapData 
	{
		int Width { get; }
		int Height { get; }

		Vector2Int Start { get; }
		Vector2Int Destination { get; }

		MapCodes[,] Map { get; }

		MapCodes GetValueIn(int x, int y);
		MapCodes GetValueIn(Vector2Int cell);
	} 
} 


