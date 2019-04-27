using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Threading;

public class GameStateController : MonoBehaviour
{
    public Tile[,] tiles;
	public Grid1 Grid;

    public Text statusText;

    public Piece piece1;
    public Piece piece2;

    public GameObject deck;

    private static GameStateController _instance;

    private Action enemyMove = null;

    public enum GameState
    {
        SelectingCard,
        SelectingMove,
        Waiting
    }

    public GameState currentState;

    public static GameStateController GetInstance()
    {
        return _instance;
    }

    private void EnemyMoved(int x, int y)
    {
        Debug.Log("Move: " + x + ", " + y);
        Debug.Log("Enemy location: " + piece2.x + ", " + piece2.y);
        piece2.Move(tiles[x, y].gameObject);
    }

    private void SetPlayer(string[] parameters)
    {
        var player = int.Parse(parameters[1]);
        statusText.text = "You are player " + player + "!";
        statusText.gameObject.SetActive(true);
        Destroy(statusText.gameObject, 2);
        if (player == 2)
            SwapPlayers();
    }

    private void TileDestroyed(int x, int y)
    {
        Debug.Log("Tile destroyed: " + x + ", " + y);
        if (tiles[x, y] != null)
        {
            Destroy(tiles[x, y].gameObject);
            tiles[x, y] = null;
        }
    }

    private void SwapPlayers()
    {
        var tmp = piece1;
        piece1 = piece2;
        piece2 = tmp;
    }

    void MoveAndDestroyHandler(string[] parameters)
    {
        if (parameters.Length != 5)
            throw new Exception("Invalid command!");
        var moveX = int.Parse(parameters[1]);
        var moveY = int.Parse(parameters[2]);
        var destroyX = int.Parse(parameters[3]);
        var destroyY = int.Parse(parameters[4]);
        enemyMove = () =>
            {
                EnemyMoved(moveX, moveY);
                TileDestroyed(destroyX, destroyY);
            };
    }

    void Start()
    {
        statusText.text = "Click on a tile to move there!";
        currentState = GameState.SelectingCard; 
        _instance = this;

        NetworkClient.RegisterHandler("MoveAndDestroy", MoveAndDestroyHandler);
        NetworkClient.RegisterHandler("SetPlayer", SetPlayer);
    }

    void Update()
    {
        if (currentState == GameState.Waiting && enemyMove != null)
        {
            enemyMove();
            currentState = GameState.SelectingCard;
            deck.SetActive(true);
        }
    }

    public Tile.TileOperation CreateOperation(int moveX, int moveY, int destroyX, int destroyY)
    {
        return (Piece piece) => {
            MovePiece(piece1, moveX, moveY);
            DestroyTile(destroyX, destroyY);
            return NetworkClient.Send("MoveAndDestroy " + moveX + " " + moveY + " " + destroyX + " " + destroyY);
        };
    }

    public void HighlightMoveOptions(int range,int tileX, int tileY)
    {
        
        //UpRight:
        var tileIndex = GetTileIndex(tileX, tileY, HexagonDirection.UpRight, range);
        
        if (HasEmptyTile(tileIndex.Item1, tileIndex.Item2))
        {
            var tile = tiles[tileIndex.Item1, tileIndex.Item2];
            tile.SetHighlight(HighlightType.Move);

            var tileIndexAtt = GetTileIndex(tileX, tileY, HexagonDirection.UpRight, range+1);
            if (HasEmptyTile(tileIndexAtt.Item1, tileIndexAtt.Item2))
            {
                var tileAtt = tiles[tileIndexAtt.Item1, tileIndexAtt.Item2];
                tileAtt.SetHighlight(HighlightType.Attack);
            }
            tile.Operation = CreateOperation(tileIndex.Item1, tileIndex.Item2, tileIndexAtt.Item1, tileIndexAtt.Item2);
        }
        

        //UpLeft:
        var tileIndex1 = GetTileIndex(tileX, tileY, HexagonDirection.UpLeft, range);

        if (HasEmptyTile(tileIndex1.Item1, tileIndex1.Item2))
        {
            var tile = tiles[tileIndex1.Item1, tileIndex1.Item2];
            tile.SetHighlight(HighlightType.Move);

            var tileIndexAtt = GetTileIndex(tileX, tileY, HexagonDirection.UpLeft, range + 1);
            if (HasEmptyTile(tileIndexAtt.Item1, tileIndexAtt.Item2))
            {
                var tileAtt = tiles[tileIndexAtt.Item1, tileIndexAtt.Item2];
                tileAtt.SetHighlight(HighlightType.Attack);
            }
            tile.Operation = CreateOperation(tileIndex1.Item1, tileIndex1.Item2, tileIndexAtt.Item1, tileIndexAtt.Item2);
        }

        //DownRight:
        var tileIndex2 = GetTileIndex(tileX, tileY, HexagonDirection.DownRight, range);

        if (HasEmptyTile(tileIndex2.Item1, tileIndex2.Item2))
        {
            var tile = tiles[tileIndex2.Item1, tileIndex2.Item2];
            tile.SetHighlight(HighlightType.Move);

            var tileIndexAtt = GetTileIndex(tileX, tileY, HexagonDirection.DownRight, range + 1);
            if (HasEmptyTile(tileIndexAtt.Item1, tileIndexAtt.Item2))
            {
                var tileAtt = tiles[tileIndexAtt.Item1, tileIndexAtt.Item2];
                tileAtt.SetHighlight(HighlightType.Attack);
            }
            tile.Operation = CreateOperation(tileIndex2.Item1, tileIndex2.Item2, tileIndexAtt.Item1, tileIndexAtt.Item2);
        }

        //DownLeft:
        var tileIndex3 = GetTileIndex(tileX, tileY, HexagonDirection.DownLeft, range);

        if (HasEmptyTile(tileIndex3.Item1, tileIndex3.Item2))
        {
            var tile = tiles[tileIndex3.Item1, tileIndex3.Item2];
            tile.SetHighlight(HighlightType.Move);

            var tileIndexAtt = GetTileIndex(tileX, tileY, HexagonDirection.DownLeft, range + 1);
            if (HasEmptyTile(tileIndexAtt.Item1, tileIndexAtt.Item2))
            {
                var tileAtt = tiles[tileIndexAtt.Item1, tileIndexAtt.Item2];
                tileAtt.SetHighlight(HighlightType.Attack);
            }
            tile.Operation = CreateOperation(tileIndex3.Item1, tileIndex3.Item2, tileIndexAtt.Item1, tileIndexAtt.Item2);
        }

        //Right:
        var tileIndex4 = GetTileIndex(tileX, tileY, HexagonDirection.Right, range);

        if (HasEmptyTile(tileIndex4.Item1, tileIndex4.Item2))
        {
            var tile = tiles[tileIndex4.Item1, tileIndex4.Item2];
            tile.SetHighlight(HighlightType.Move);

            var tileIndexAtt = GetTileIndex(tileX, tileY, HexagonDirection.Right, range + 1);
            if (HasEmptyTile(tileIndexAtt.Item1, tileIndexAtt.Item2))
            {
                var tileAtt = tiles[tileIndexAtt.Item1, tileIndexAtt.Item2];
                tileAtt.SetHighlight(HighlightType.Attack);
            }
            tile.Operation = CreateOperation(tileIndex4.Item1, tileIndex4.Item2, tileIndexAtt.Item1, tileIndexAtt.Item2);
        }

        //Left:
        var tileIndex5 = GetTileIndex(tileX, tileY, HexagonDirection.Left, range);

        if (HasEmptyTile(tileIndex5.Item1, tileIndex5.Item2))
        {
            var tile = tiles[tileIndex5.Item1, tileIndex5.Item2];
            tile.SetHighlight(HighlightType.Move);

            var tileIndexAtt = GetTileIndex(tileX, tileY, HexagonDirection.Left, range + 1);
            if (HasEmptyTile(tileIndexAtt.Item1, tileIndexAtt.Item2))
            {
                var tileAtt = tiles[tileIndexAtt.Item1, tileIndexAtt.Item2];
                tileAtt.SetHighlight(HighlightType.Attack);
            }
            tile.Operation = CreateOperation(tileIndex5.Item1, tileIndex5.Item2, tileIndexAtt.Item1, tileIndexAtt.Item2);
        }
    }

    public void CardSelected(Card card)
    {
        currentState = GameState.SelectingMove;
        deck.SetActive(false);
        HighlightMoveOptions(card.moveAmount, piece1.x, piece1.y);
    }

    public void DeleteTile(int x, int y)
    {
        var tile = tiles[x, y];
        GameObject.Destroy(tile.gameObject);
        tiles[x, y] = null;
    }

    private void ClearHighlights()
    {
        foreach (var tile in tiles) {
            if (tile != null)
                tile.SetHighlight(HighlightType.None);
        }
    }

    private void DestroyTile(int x, int y)
    {
        TileDestroyed(x, y);
    }

    private void MovePiece(Piece piece, int x, int y)
    {
        var tile = tiles[x, y];
        piece.Move(tile.gameObject);
    }

    public Task MoveSelected(Tile tile)
    {
        currentState = GameState.Waiting;
        ClearHighlights();
        return tile.Operation(piece1);
    }

    public async Task TileHit(Tile tile)
    {
        if (currentState == GameState.SelectingMove &&
            tile.moveHightligt.activeSelf)
            await MoveSelected(tile);
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
