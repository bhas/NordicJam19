using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x;
    public int y;
	public GameObject moveHightligt;
	public GameObject attackHightligt;

    public GameStateController gameStateController;

    public async delegate void TileOperation(Piece piece);

    public TileOperation Operation;

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
