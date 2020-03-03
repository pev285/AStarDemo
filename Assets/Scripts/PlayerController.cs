using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo
{
	public class PlayerController : MonoBehaviour 
	{
		[SerializeField]
		private Camera _camera;

		private void Awake()
		{
			if (_camera == null)
				_camera = Camera.main;

			MessageBuss.Input.GetTouchPosition += GetPoint;

			MessageBuss.Input.GetTouch += GetToucch;
			MessageBuss.Input.GetTouchDown += GetTouchDown;
		}

		private void OnDestroy()
		{
			MessageBuss.Input.GetTouchPosition -= GetPoint;

			MessageBuss.Input.GetTouch -= GetToucch;
			MessageBuss.Input.GetTouchDown -= GetTouchDown;
		}

		//private void Update()
		//{
		//	var point = GetPoint();
		//	Debug.Log($"point={point}");

		//}

		private bool GetTouchDown()
		{
			return Input.GetMouseButtonDown(0);
		}

		private bool GetToucch()
		{
			return Input.GetMouseButton(0);
		}

		private Vector2 GetPoint()
		{
			var shift = new Vector3(0.5f, 0.5f);
			var size = MessageBuss.Screen.GetMinDimension();

			Vector3 position = _camera.ScreenToWorldPoint(Input.mousePosition);
			return (position) / size + shift;
		}
	} 
} 


