using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public Tuple<int, int> PositionIndex;
	public GameObject moveHightligt;
	public GameObject attackHightligt;

	public void SetHighlight(HighlightType type)
	{
		moveHightligt.SetActive(type == HighlightType.Move);
		attackHightligt.SetActive(type == HighlightType.Attack);
	}
}

public enum HighlightType
{
	None, Move, Attack
}
