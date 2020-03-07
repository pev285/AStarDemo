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
			MessageBuss.Global.SystemStateChanged += HandleStateChange;
		}

		private void Start()
		{
			_nextButton.onClick.AddListener(NotifyNextPressed);
			_startOverButton.onClick.AddListener(NotifyStartOver);
		}

		private void HandleStateChange(SystemState state)
		{
			_stateText.text = _hints[state];
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
		}



	} 
} 


