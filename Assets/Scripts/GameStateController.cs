using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public Tile[,] tiles;
	public Grid1 Grid;

    void Start()
    {
	}

    public void CardSelected()
    {

    }

	public void Test(Tile[,] tiles)
	{
		this.tiles = tiles;
		print("Go");
		var tileIndex = GetTileIndex(4,4, HexagonDirection.UpRight, 3);
		print(tileIndex);
		print(tiles.GetLength(0));
		print(tiles.GetLength(1));
		if (HasEmptyTile(tileIndex.Item1, tileIndex.Item2))
		{
			print("Found it");
			var tile = tiles[tileIndex.Item1, tileIndex.Item2];
			tile.SetHighlight(HighlightType.Attack);
		}
	}
		
	public Tuple<int, int> GetTileIndex(int posx, int posy, HexagonDirection dir, int range)
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

		return Tuple.Create(posx + offsetX, posy + offsetY);
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
