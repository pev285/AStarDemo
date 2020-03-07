using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo.PathFinding
{
	public interface IPathFinder
	{
		event Action<bool> SearchCompleted;
		event Action<Vector2Int> PathNodeFound;

		event Action<Vector2Int> NodeOpened;
		event Action<Vector2Int> NodeClosed;


		bool IsInProgress { get; }

		void Stop();
		void Start(MapData data);

		List<Vector2Int> GetResults();	
	} 
} 


