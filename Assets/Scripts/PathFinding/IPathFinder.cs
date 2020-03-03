using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo.PathFinding
{
	public interface IPathFinder
	{
		IReadOnlyList<Vector2Int> GetPath(Vector2Int start, Vector2Int destination, IMapData data);
	

	} 
} 


