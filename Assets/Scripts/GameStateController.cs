using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    public Tile[,] tiles;
	public Grid1 Grid;

    public Text statusText;

    public Piece piece1;
    public Piece piece2;

    public enum GameState
    {
        SelectingCard,
        SelectingMove
    }

    public GameState currentState;

    void Start()
    {
        statusText.text = "Click on a tile to move there!";
        currentState = GameState.SelectingCard;
    }

    public void HighlightMoveOptions(int range,int tileX, int tileY)
    {

        //UpRight:
        var tileIndex = GetTileIndex(tileX, tileY, HexagonDirection.UpRight, range);
        
        if (HasEmptyTile(tileIndex.Item1, tileIndex.Item2))
        {
            var tile = tiles[tileIndex.Item1, tileIndex.Item2];
            tile.SetHighlight(HighlightType.Move);
        }
        

        //UpLeft:
        var tileIndex1 = GetTileIndex(tileX, tileY, HexagonDirection.UpLeft, range);

        if (HasEmptyTile(tileIndex1.Item1, tileIndex1.Item2))
        {
            var tile = tiles[tileIndex1.Item1, tileIndex1.Item2];
            tile.SetHighlight(HighlightType.Move);
        }

        //DownRight:
        var tileIndex2 = GetTileIndex(tileX, tileY, HexagonDirection.DownRight, range);

        if (HasEmptyTile(tileIndex2.Item1, tileIndex2.Item2))
        {
            var tile = tiles[tileIndex2.Item1, tileIndex2.Item2];
            tile.SetHighlight(HighlightType.Move);
        }

        //DownLeft:
        var tileIndex3 = GetTileIndex(tileX, tileY, HexagonDirection.DownLeft, range);

        if (HasEmptyTile(tileIndex3.Item1, tileIndex3.Item2))
        {
            var tile = tiles[tileIndex3.Item1, tileIndex3.Item2];
            tile.SetHighlight(HighlightType.Move);
        }

        //Right:
        var tileIndex4 = GetTileIndex(tileX, tileY, HexagonDirection.Right, range);

        if (HasEmptyTile(tileIndex4.Item1, tileIndex4.Item2))
        {
            var tile = tiles[tileIndex4.Item1, tileIndex4.Item2];
            tile.SetHighlight(HighlightType.Move);
        }

        //Left:
        var tileIndex5 = GetTileIndex(tileX, tileY, HexagonDirection.Left, range);

        if (HasEmptyTile(tileIndex5.Item1, tileIndex5.Item2))
        {
            var tile = tiles[tileIndex5.Item1, tileIndex5.Item2];
            tile.SetHighlight(HighlightType.Move);
        }

    }

    public void CardSelected(Card card)
    {
        currentState = GameState.SelectingMove;
    }

    public void MoveSelected(Tile tile)
    {
        //piece1.Move(tile.gameObject);
        statusText.text = "";
        currentState = GameState.SelectingCard;
    }

    public void DeleteTile(int x, int y)
    {
        var tile = tiles[x, y];
        GameObject.Destroy(tile.gameObject);
        tiles[x, y] = null;
    }

    public void TileHit(Tile tile)
    {
        if (currentState == GameState.SelectingMove)
            MoveSelected(tile);
    }

	public void Test(Tile[,] tiles)
	{
		this.tiles = tiles;
		print("Go");
		var tileIndex = GetTileIndex(7,7, HexagonDirection.DownRight, 2);
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
				offsetY = -range;
				offsetX = range / 2;
				if (range % 2 == 1 && posy % 2 == 1)
					offsetX++;
				break;
			case HexagonDirection.DownLeft:
				offsetY = -range;
				offsetX = -range / 2;
				if (range % 2 == 1 && posy % 2 == 0)
					offsetX--;
				break;
			case HexagonDirection.Right:
				offsetX = range;
				break;
			case HexagonDirection.Left:
				offsetX = -range;
				break;
		}

		return Tuple.Create(posx + offsetX, posy + offsetY);
	}

	public bool HasEmptyTile(int x, int y)
	{
		if (x < 0 || x >= tiles.GetLength(0))
			return false;
		if (y < 0 || y >= tiles.GetLength(1))
			return false;

		return tiles[x, y] != null;
	}   
}

public enum HexagonDirection
{
	Left, Right, UpLeft, UpRight, DownLeft, DownRight
}
