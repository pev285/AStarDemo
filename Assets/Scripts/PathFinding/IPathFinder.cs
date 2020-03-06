using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo.PathFinding
{
	public interface IPathFinder
	{
		event Action SearchCompleted;

		bool IsInProgress { get; }

		void Stop();
		void Start(IMapData data);

		IMapData GetResults();	
	} 
} 


