using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfDoomCard : MonoBehaviour, ICardOperation
{
    private static string CreateCommand(int moveX, int moveY, List<Tuple<int, int>> doomLine)
    {
        var builder = new StringBuilder();
        builder.Append("DoomLine " + moveX + " " + moveY);
        foreach (var pair in doomLine)
            builder.Append(" " + pair.Item1 + " " + pair.Item2);
        return builder.ToString();
    }

    public static void ExecuteLineOfDoom(Piece piece, int moveX, int moveY, List<Tuple<int, int>> doomLine)
    {
        var gameController = GameStateController.GetInstance();
        gameController.MovePiece(piece, moveX, moveY);
        foreach (var pair in doomLine)
            gameController.DestroyTile(pair.Item1, pair.Item2);
    }

    private static Tile.TileOperation CreateOperation(int moveX, int moveY, List<Tuple<int, int>> doomLine)
    {
        return (Piece piece) => {
            var gameController = GameStateController.GetInstance();
            ExecuteLineOfDoom(gameController.piece1, moveX, moveY, doomLine);
            return NetworkClient.Send(CreateCommand(moveX, moveY, doomLine));
        };
    }

    public void HighlightMoveOptions(int range, int tileX, int tileY)
    {
        var gameController = GameStateController.GetInstance();
        var tiles = gameController.tiles;

        //UpRight:
        var tileIndex = GetTileIndex(tileX, tileY, HexagonDirection.UpRight, range);

        if (HasTile(tileIndex.Item1, tileIndex.Item2))
        {
            var tile = tiles[tileIndex.Item1, tileIndex.Item2];
            tile.SetHighlight(HighlightType.Move);

            var doomLine = new List<Tuple<int, int>>();
            for (int i = 1; i < 100; i++)
            {
                var tileIndexAtt = GetTileIndex(tileX, tileY, HexagonDirection.UpRight, range + i);
                if (HasTile(tileIndexAtt.Item1, tileIndexAtt.Item2))
                {
                    var tileAtt = tiles[tileIndexAtt.Item1, tileIndexAtt.Item2];
                    tileAtt.SetHighlight(HighlightType.Attack);
                    doomLine.Add(tileIndexAtt);
                }
            }
            tile.Operation = CreateOperation(tileIndex.Item1, tileIndex.Item2, doomLine);
        }


        //UpLeft:
        var tileIndex1 = GetTileIndex(tileX, tileY, HexagonDirection.UpLeft, range);

        if (HasTile(tileIndex1.Item1, tileIndex1.Item2))
        {
            var tile = tiles[tileIndex1.Item1, tileIndex1.Item2];
            tile.SetHighlight(HighlightType.Move);

            var doomLine = new List<Tuple<int, int>>();
            for (int i = 1; i < 100; i++)
            {
                var tileIndexAtt = GetTileIndex(tileX, tileY, HexagonDirection.UpLeft, range + i);
                if (HasTile(tileIndexAtt.Item1, tileIndexAtt.Item2))
                {
                    var tileAtt = tiles[tileIndexAtt.Item1, tileIndexAtt.Item2];
                    tileAtt.SetHighlight(HighlightType.Attack);
                    doomLine.Add(tileIndexAtt);
                }
            }
            tile.Operation = CreateOperation(tileIndex1.Item1, tileIndex1.Item2, doomLine);
        }

        //DownRight:
        var tileIndex2 = GetTileIndex(tileX, tileY, HexagonDirection.DownRight, range);

        if (HasTile(tileIndex2.Item1, tileIndex2.Item2))
        {
            var tile = tiles[tileIndex2.Item1, tileIndex2.Item2];
            tile.SetHighlight(HighlightType.Move);

            var doomLine = new List<Tuple<int, int>>();
            for (int i = 1; i < 100; i++)
            {
                var tileIndexAtt = GetTileIndex(tileX, tileY, HexagonDirection.DownRight, range + i);
                if (HasTile(tileIndexAtt.Item1, tileIndexAtt.Item2))
                {
                    var tileAtt = tiles[tileIndexAtt.Item1, tileIndexAtt.Item2];
                    tileAtt.SetHighlight(HighlightType.Attack);
                    doomLine.Add(tileIndexAtt);
                }
            }
            tile.Operation = CreateOperation(tileIndex2.Item1, tileIndex2.Item2, doomLine);
        }

        //DownLeft:
        var tileIndex3 = GetTileIndex(tileX, tileY, HexagonDirection.DownLeft, range);

        if (HasTile(tileIndex3.Item1, tileIndex3.Item2))
        {
            var tile = tiles[tileIndex3.Item1, tileIndex3.Item2];
            tile.SetHighlight(HighlightType.Move);

            var doomLine = new List<Tuple<int, int>>();
            for (int i = 1; i < 100; i++)
            {
                var tileIndexAtt = GetTileIndex(tileX, tileY, HexagonDirection.DownLeft, range + i);
                if (HasTile(tileIndexAtt.Item1, tileIndexAtt.Item2))
                {
                    var tileAtt = tiles[tileIndexAtt.Item1, tileIndexAtt.Item2];
                    tileAtt.SetHighlight(HighlightType.Attack);
                    doomLine.Add(tileIndexAtt);
                }
            }
            tile.Operation = CreateOperation(tileIndex3.Item1, tileIndex3.Item2, doomLine);
        }

        //Right:
        var tileIndex4 = GetTileIndex(tileX, tileY, HexagonDirection.Right, range);

        if (HasTile(tileIndex4.Item1, tileIndex4.Item2))
        {
            var tile = tiles[tileIndex4.Item1, tileIndex4.Item2];
            tile.SetHighlight(HighlightType.Move);

            var doomLine = new List<Tuple<int, int>>();
            for (int i = 1; i < 100; i++)
            {
                var tileIndexAtt = GetTileIndex(tileX, tileY, HexagonDirection.Right, range + i);
                if (HasTile(tileIndexAtt.Item1, tileIndexAtt.Item2))
                {
                    var tileAtt = tiles[tileIndexAtt.Item1, tileIndexAtt.Item2];
                    tileAtt.SetHighlight(HighlightType.Attack);
                    doomLine.Add(tileIndexAtt);
                }
            }
            tile.Operation = CreateOperation(tileIndex4.Item1, tileIndex4.Item2, doomLine);
        }

        //Left:
        var tileIndex5 = GetTileIndex(tileX, tileY, HexagonDirection.Left, range);

        if (HasTile(tileIndex5.Item1, tileIndex5.Item2))
        {
            var tile = tiles[tileIndex5.Item1, tileIndex5.Item2];
            tile.SetHighlight(HighlightType.Move);

            var doomLine = new List<Tuple<int, int>>();
            for (int i = 1; i < 100; i++)
            {
                var tileIndexAtt = GetTileIndex(tileX, tileY, HexagonDirection.Left, range + i);
                if (HasTile(tileIndexAtt.Item1, tileIndexAtt.Item2))
                {
                    var tileAtt = tiles[tileIndexAtt.Item1, tileIndexAtt.Item2];
                    tileAtt.SetHighlight(HighlightType.Attack);
                    doomLine.Add(tileIndexAtt);
                }
            }
            tile.Operation = CreateOperation(tileIndex5.Item1, tileIndex5.Item2, doomLine);
        }
    }

    bool HasTile(int x, int y)
    {
        var gameController = GameStateController.GetInstance();
        return gameController.HasTile(x, y);
    }

    public static Tuple<int, int> GetTileIndex(int posx, int posy, HexagonDirection dir, int range)
    {
        return TileIndexComputer.GetTileIndex(posx, posy, dir, range);
    }
}
