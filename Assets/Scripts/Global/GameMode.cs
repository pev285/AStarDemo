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

		private IPathFinder _searcher = new SimpleAStar();

		private void Awake()
		{
			var obj = Instantiate(MapPrefab);
			_map = obj.GetComponent<MapController>();
		}

		private void Start()
		{
			Reset();

			MessageBuss.Input.StartOver += Reset;
			MessageBuss.Input.NextStage += GoToNextState;
		}

		private void OnDestroy()
		{
			MessageBuss.Input.StartOver -= Reset;
			MessageBuss.Input.NextStage -= GoToNextState;
		}

		private void GoToNextState()
		{
			switch (_state)
			{
				case SystemState.LocateObstacles:
					TransitionTo(SystemState.LocateStart);
					break;
				case SystemState.LocateStart:
					TransitionTo(SystemState.LocateDestination);
					break;
				case SystemState.LocateDestination:
					TransitionTo(SystemState.LookForBestPath);
					break;
			}
		}

		private void TransitionTo(SystemState state)
		{
			_state = state;
			MessageBuss.Global.SystemStateChanged(_state);
		}

		private void Reset()
		{
			_searcher.Stop();
			_searcher.SearchCompleted -= ShowResults;

			_map.Clear();
			TransitionTo(SystemState.LocateObstacles);
		}

		private void ShowResults()
		{
			_searcher.SearchCompleted -= ShowResults;
			TransitionTo(SystemState.ShowResults);
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
			// Simply showing //
		}

		private void UpdateLookForBestPath()
		{
			if (_searcher.IsInProgress)
				return;

			var data = _map.GetData();
			_searcher.SearchCompleted += ShowResults;

			_searcher.Start(data);
		}

		private void UpdateLocateDestination()
		{
		}

		private void UpdateLocateStart()
		{
		}

		private void UpdateLocateObstacles()
		{
			if (MessageBuss.Input.GetTouch())
			{
				var point = MessageBuss.Input.GetTouchPosition();
				_map.SetObstacle(point);
			}
		}
	}
} 


