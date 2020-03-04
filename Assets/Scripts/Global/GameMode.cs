using AStarDemo.PathFinding;
using AStarDemo.Visualization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo.Global
{
	public class GameMode : MonoBehaviour 
	{

		[SerializeField]
		private GameObject MapPrefab;

		private SystemState _state;
		private MapController _map;

		private IPathFinder _searcher;

		private void Awake()
		{
			var obj = Instantiate(MapPrefab);
			_map = obj.GetComponent<MapController>();
		}

		private void Start()
		{
			Reset();
		}

		private void TransitionTo(SystemState state)
		{
			_state = state;
			MessageBuss.Global.SystemStateChanged(_state);
		}

		private void Reset()
		{
			_map.Clear();
			TransitionTo(SystemState.LocateObstacles);
		}


		private void Update()
		{
			switch (_state)
			{
				case SystemState.LocateObstacles:
					UpdateLocateObstacles();
					break;
				case SystemState.LocateStart:
					UpdateLocateStart();
					break;
				case SystemState.LocateDestination:
					UpdateLocateDestination();
					break;
				case SystemState.LookForBestPath:
					UpdateLookForBestPath();
					break;
				case SystemState.ShowResults:
					UpdateShowResults();
					break;
				default:
					throw new NotImplementedException($"Unexpected state {_state}");
			}
		}

		private void UpdateShowResults()
		{
			throw new NotImplementedException();
		}

		private void UpdateLookForBestPath()
		{
			throw new NotImplementedException();
		}

		private void UpdateLocateDestination()
		{
			throw new NotImplementedException();
		}

		private void UpdateLocateStart()
		{
			throw new NotImplementedException();
		}

		private void UpdateLocateObstacles()
		{
			throw new NotImplementedException();
		}


	} 
} 


