using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo.PathFinding
{
	public class SimpleAStar : IPathFinder
	{
		public event Action SearchCompleted = () => { };

		public bool IsInProgress { get; private set; } = false;

		public IMapData GetResults()
		{
			throw new NotImplementedException();
		}

		public void Stop()
		{
			IsInProgress = false;
		}

		public void Start(IMapData data)
		{
			IsInProgress = true;
			throw new NotImplementedException();
		}
	}
} 


