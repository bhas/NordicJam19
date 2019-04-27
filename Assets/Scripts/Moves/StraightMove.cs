using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMove : MonoBehaviour, IMoveAction
{
	public int range;

	public List<Tuple<int, int>> GetMovePatterns()
	{
		var patterns = new List<Tuple<int, int>>();
		switch (range)
		{
			case 1:
				patterns.Add(Tuple.Create(-1, 0));
				patterns.Add(Tuple.Create(1, 0));
				patterns.Add(Tuple.Create(-1, 1));
				patterns.Add(Tuple.Create(0, 1));
				patterns.Add(Tuple.Create(-1, -1));
				patterns.Add(Tuple.Create(0, -1));
				break;
		}
		return patterns;
	}
}
