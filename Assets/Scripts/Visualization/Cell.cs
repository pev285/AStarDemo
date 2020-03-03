using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo.Visualization
{
	public class Cell : MonoBehaviour 
	{
		private Renderer _renderer;

		private void Awake()
		{
			_renderer = GetComponent<Renderer>();
		}

		public void SetColor(Color color)
		{
			_renderer.material.color = color;
		}
	
	} 
} 


