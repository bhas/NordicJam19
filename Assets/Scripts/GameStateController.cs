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

    private ICardOperation cardOperation = new MoveAndDestroyCard();

    private static GameStateController _instance;

    private Action enemyMove = null;

    private static bool isPlayer2 = false;

    public enum GameState
    {
        SelectingCard,
        SelectingMove,
        Waiting,
        GameOver
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
        Vector2 gridPos = new Vector2(x, y);
        piece2.Move(x, y, Grid1.CalcWorldPos(gridPos));
    }

    IEnumerator HideStatusTextAfterTwoSeconds()
    {
        yield return new WaitForSeconds(2);
        statusText.gameObject.SetActive(false);
    }

    private void SetPlayer(string[] parameters)
    {
        var player = int.Parse(parameters[1]);
        statusText.text = "You are player " + player + "!";
        statusText.gameObject.SetActive(true);
        StartCoroutine(HideStatusTextAfterTwoSeconds());
        if (player == 2)
            SwapPlayers();
    }

    private void TileDestroyed(int x, int y)
    {
        Debug.Log("Tile destroyed: " + x + ", " + y);
        if (tiles[x, y] != null)
        {
			StartCoroutine(tiles[x, y].AnimateAndDestroy());
            tiles[x, y] = null;
        }
    }

    private void SwapPlayers()
    {
        var tmp = piece1;
        piece1 = piece2;
        piece2 = tmp;
        isPlayer2 = true;
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

    void LineOfDoomHandler(string[] parameters)
    {
        var moveX = int.Parse(parameters[1]);
        var moveY = int.Parse(parameters[2]);
        var doomLine = new List<Tuple< int, int>>();
        for (var i = 3; i < parameters.Length; i += 2)
        {
            var x = int.Parse(parameters[i]);
            var y = int.Parse(parameters[i + 1]);
            doomLine.Add(new Tuple<int, int>(x, y));
        }
        enemyMove = () =>
        {
            LineOfDoomCard.ExecuteLineOfDoom(piece2, moveX, moveY, doomLine);
        };
    }

    void Start()
    {
        statusText.text = "Click on a tile to move there!";
        currentState = GameState.SelectingCard; 
        _instance = this;

        NetworkClient.RegisterHandler("MoveAndDestroy", MoveAndDestroyHandler);
        NetworkClient.RegisterHandler("DoomLine", LineOfDoomHandler);
        NetworkClient.RegisterHandler("SetPlayer", SetPlayer);
    }

    void YouWon()
    {
        statusText.text = "You won! Yay!";
        statusText.gameObject.SetActive(true);
        Destroy(piece2);
        currentState = GameState.GameOver;
    }

    void YouLost()
    {
        statusText.text = "You lost! Shame!";
        statusText.gameObject.SetActive(true);
        Destroy(piece1);
        currentState = GameState.GameOver;
    }

    void Draw()
    {
        statusText.text = "It's a draw! A pox on both your houses!";
        statusText.gameObject.SetActive(true);
        currentState = GameState.GameOver;
    }

    void Update()
    {
        if (currentState == GameState.Waiting && enemyMove != null)
        {
            enemyMove();
            currentState = GameState.SelectingCard;
            deck.SetActive(true);
            enemyMove = null;

            if ((piece1.x == piece2.x && piece1.y == piece2.y) ||
                (!HasTile(piece1.x, piece1.y) && !HasTile(piece2.x, piece2.y)))
                Draw();
            else {
                if (!HasTile(piece1.x, piece1.y))
                    YouLost();

                if (!HasTile(piece2.x, piece2.y))
                    YouWon();
            }
        }
    }

    public void CardSelected(Card card)
    {
        currentState = GameState.SelectingMove;
        deck.SetActive(false);
        cardOperation.HighlightMoveOptions(card.moveAmount, piece1.x, piece1.y);
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

    public void DestroyTile(int x, int y)
    {
        TileDestroyed(x, y);
    }

    public void MovePiece(Piece piece, int x, int y)
    {
        Vector2 gridPos = new Vector2(x, y);
        piece.Move(x, y, Grid1.CalcWorldPos(gridPos));
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

	public bool HasTile(int x, int y)
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
