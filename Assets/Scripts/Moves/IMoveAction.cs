using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveAction
{
	List<Tuple<int, int>> GetMovePatterns();
}
