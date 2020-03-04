using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo.PathFinding
{
	public class SimpleAStar : IPathFinder
	{
		public event Action SearchCompleted = () => { };

		public IMapData GetResults()
		{
			throw new NotImplementedException();
		}

		public void StartSearch(IMapData data)
		{
			throw new NotImplementedException();
		}
	}
} 


