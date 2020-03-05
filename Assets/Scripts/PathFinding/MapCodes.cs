﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarDemo.PathFinding 
{
	public enum MapCodes : byte 
	{
		Empty = 0,
		Obstacle = 1,
		Start = 2,
		Destination = 3,
		PathPoint = 4,

		Candidate = 5,
		Evaluated = 6,
	} 
} 


