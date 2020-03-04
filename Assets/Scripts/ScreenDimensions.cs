using AStarDemo.Global;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo
{
	public class ScreenDimensions : MonoBehaviour 
	{
		[SerializeField]
		private Camera _camera;

		private float _min;
		private float _width;
		private float _height;

		private void Awake()
		{
			if (_camera == null)
				_camera = Camera.main;

			EvaluateFields();
			Subscribe();
		}

		private void EvaluateFields()
		{
			_height = 2.0f * _camera.orthographicSize;
			_width = _height * _camera.aspect;

			_min = Mathf.Min(_width, _height);
		}

		private void Start()
		{
		}

		private void OnDestroy()
		{
			Unsubscribe();
		}

		private void Subscribe()
		{
			MessageBuss.Screen.GetWorldWidth += GetWorldWidth;
			MessageBuss.Screen.GetWorldHeight += GetWorldHeight;

			MessageBuss.Screen.GetMinDimension += GetMin;
		}

		private void Unsubscribe()
		{
			MessageBuss.Screen.GetWorldWidth -= GetWorldWidth;
			MessageBuss.Screen.GetWorldHeight -= GetWorldHeight;

			MessageBuss.Screen.GetMinDimension -= GetMin;
		}

		private float GetMin()
		{
			return _min;
		}

		private float GetWorldHeight()
		{
			return _height;
		}

		private float GetWorldWidth()
		{
			return _width;
		}
	} 
} 


