using AStarDemo.Global;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AStarDemo.UserInteractions
{
	public class UI : MonoBehaviour 
	{
		[SerializeField]
		private Button _nextButton;
		[SerializeField]
		private Button _startOverButton;

		[SerializeField]
		private Text _stateText;

		private Dictionary<SystemState, string> _hints = new Dictionary<SystemState, string>
		{
			{ SystemState.LocateObstacles, "Расставьте препятствия" },
			{ SystemState.LocateStart, "Выберите место для старта" },
			{ SystemState.LocateDestination, "Выберите место для финиша" },
			{ SystemState.LookForBestPath, "Ведутся рассчеты" },
			{ SystemState.ShowResults, "Готово!!!" },
			{ SystemState.SearchFailed, "Не удалось найти подходящий путь :(" },
		};

		private void Awake()
		{
			_nextButton.onClick.AddListener(NotifyNextPressed);
			_startOverButton.onClick.AddListener(NotifyStartOver);

			MessageBuss.Global.SystemStateChanged += HandleStateChange;

			MessageBuss.Map.StartPositionChosen += UnblockNextButton;
			MessageBuss.Map.DestinationPositionChosen += UnblockNextButton;
		}

		private void HandleStateChange(SystemState state)
		{
			if (_hints.ContainsKey(state))
				_stateText.text = _hints[state];

			if ((state == SystemState.LocateStart || state == SystemState.LocateDestination)
				|| state == SystemState.LookForBestPath)
				_nextButton.interactable = false;

			if (state == SystemState.LocateObstacles)
				_nextButton.interactable = true;
		}

		private void UnblockNextButton(Vector2Int node)
		{
			_nextButton.interactable = true;
		}


		private void NotifyStartOver()
		{
			MessageBuss.Input.StartOver();
		}

		private void NotifyNextPressed()
		{
			MessageBuss.Input.NextStage();
		}

		private void OnDestroy()
		{
			_nextButton.onClick.RemoveListener(NotifyNextPressed);
			_startOverButton.onClick.RemoveListener(NotifyStartOver);

			MessageBuss.Global.SystemStateChanged -= HandleStateChange;

			MessageBuss.Map.StartPositionChosen += UnblockNextButton;
			MessageBuss.Map.DestinationPositionChosen += UnblockNextButton;
		}
	} 
} 


