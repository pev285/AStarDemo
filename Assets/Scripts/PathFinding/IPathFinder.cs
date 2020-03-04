using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo.PathFinding
{
	public interface IPathFinder
	{
		event Action SearchCompleted;

		void StartSearch(IMapData data);
		IMapData GetResults();	
	} 
} 


