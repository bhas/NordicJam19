using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public Tile[,] tiles;
    public Tile testTile1;
    public Tile testTile2;
    public Tile testTile3;
    public Tile testTile4;

    void Start()
    {
        testTile1.SetHighlight(HighlightType.Move);
        testTile2.SetHighlight(HighlightType.Attack);
    }

    public void CardSelected()
    {

    }

	public bool IsTileFree(int posx, int posy, HexagonDirection dir, int range)
	{
		var offsetX = 0;
		var offsetY = 0;

		switch (dir)
		{
			case HexagonDirection.UpRight:
				offsetY = range;
				offsetX = range / 2;
				if (range % 2 == 1 && posy % 2 == 1)
					offsetX++;
				break;
			case HexagonDirection.UpLeft:
				offsetY = range;
				offsetX = -range / 2;
				if (range % 2 == 1 && posy % 2 == 0)
					offsetX--;
				break;
			case HexagonDirection.DownRight:
				offsetY = range;
				offsetX = -range / 2;
				if (range % 2 == 1 && posy % 2 == 1)
					offsetX--;
				break;
			case HexagonDirection.DownLeft:
				offsetY = range;
				offsetX = -range / 2;
				if (range % 2 == 1 && posy % 2 == 0)
					offsetX--;
				break;
			case HexagonDirection.Right:
				offsetX = range;
				break;
			case HexagonDirection.Left:
				offsetY = -range;
				break;
		}

		return HasEmptyTile(posx + offsetX, posy + offsetY);
	}

	public bool HasEmptyTile(int x, int y)
	{
		if (x < 0 || x > tiles.GetLength(0))
			return false;
		if (y < 0 || y > tiles.GetLength(1))
			return false;

		return tiles[x, y] != null;
	}
}

public enum HexagonDirection
{
	Left, Right, UpLeft, UpRight, DownLeft, DownRight
}
