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
		[SerializeField]
		private GameObject UIPrefab;

		private SystemState _state;
		private MapController _map;

		private IPathFinder _searcher = new SimpleAStar();

		private void Awake()
		{
			var obj = Instantiate(MapPrefab);
			_map = obj.GetComponent<MapController>();

			Instantiate(UIPrefab);
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
			UnsubscribeSearcher();

			_map.Clear();
			TransitionTo(SystemState.LocateObstacles);
		}

		private void DemonstraightSearchResults(bool successful)
		{
			UnsubscribeSearcher();

			var path = _searcher.GetResults();
			_map.DrawPath(path);

			if (successful)
				TransitionTo(SystemState.ShowResults);
			else
				TransitionTo(SystemState.SearchFailed);
		}

		private void SubscribeSearcher()
		{
			_searcher.SearchCompleted += DemonstraightSearchResults;

			_searcher.NodeOpened += DrawOpenedCell;
			_searcher.NodeClosed += DrawClosedCell;
			_searcher.PathNodeFound += DrawPathNode;
		}

		private void DrawPathNode(Vector2Int coord)
		{
			_map.SetPathCell(coord);
		}

		private void DrawClosedCell(Vector2Int coord)
		{
			_map.SetClosedCell(coord);
		}

		private void DrawOpenedCell(Vector2Int coord)
		{
			_map.SetOpenedCell(coord);
		}

		private void UnsubscribeSearcher()
		{
			_searcher.SearchCompleted -= DemonstraightSearchResults;
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
				case SystemState.SearchFailed:
					UpdateSearchFailed();
					break;
				default:
					throw new NotImplementedException($"Unexpected state {_state}");
			}
		}

		private void UpdateSearchFailed()
		{
		}

		private void UpdateShowResults()
		{
		}

		private void UpdateLookForBestPath()
		{
			if (_searcher.IsInProgress)
				return;

			var data = _map.GetData();

			SubscribeSearcher();
			_searcher.Start(data);
		}

		private void UpdateLocateDestination()
		{
			if (MessageBuss.Input.GetTouch())
			{
				var point = MessageBuss.Input.GetTouchPosition();
				_map.SetDestination(point);
			}
		}

		private void UpdateLocateStart()
		{
			if (MessageBuss.Input.GetTouch())
			{
				var point = MessageBuss.Input.GetTouchPosition();
				_map.SetStart(point);
			}
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


