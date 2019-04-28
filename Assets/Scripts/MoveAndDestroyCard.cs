using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndDestroyCard : MonoBehaviour, ICardOperation
{
    private static Tile.TileOperation CreateOperation(int moveX, int moveY, int destroyX, int destroyY)
    {
        var gameController = GameStateController.GetInstance();
        return (Piece piece) => {
            gameController.MovePiece(gameController.piece1, moveX, moveY);
            gameController.DestroyTile(destroyX, destroyY);
            return NetworkClient.Send("MoveAndDestroy " + moveX + " " + moveY + " " + destroyX + " " + destroyY);
        };
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

            var tileIndexAtt = GetTileIndex(tileX, tileY, HexagonDirection.UpRight, range + 1);
            if (HasTile(tileIndexAtt.Item1, tileIndexAtt.Item2))
            {
                var tileAtt = tiles[tileIndexAtt.Item1, tileIndexAtt.Item2];
                tileAtt.SetHighlight(HighlightType.Attack);
            }
            tile.Operation = CreateOperation(tileIndex.Item1, tileIndex.Item2, tileIndexAtt.Item1, tileIndexAtt.Item2);
        }


        //UpLeft:
        var tileIndex1 = GetTileIndex(tileX, tileY, HexagonDirection.UpLeft, range);

        if (HasTile(tileIndex1.Item1, tileIndex1.Item2))
        {
            var tile = tiles[tileIndex1.Item1, tileIndex1.Item2];
            tile.SetHighlight(HighlightType.Move);

            var tileIndexAtt = GetTileIndex(tileX, tileY, HexagonDirection.UpLeft, range + 1);
            if (HasTile(tileIndexAtt.Item1, tileIndexAtt.Item2))
            {
                var tileAtt = tiles[tileIndexAtt.Item1, tileIndexAtt.Item2];
                tileAtt.SetHighlight(HighlightType.Attack);
            }
            tile.Operation = CreateOperation(tileIndex1.Item1, tileIndex1.Item2, tileIndexAtt.Item1, tileIndexAtt.Item2);
        }

        //DownRight:
        var tileIndex2 = GetTileIndex(tileX, tileY, HexagonDirection.DownRight, range);

        if (HasTile(tileIndex2.Item1, tileIndex2.Item2))
        {
            var tile = tiles[tileIndex2.Item1, tileIndex2.Item2];
            tile.SetHighlight(HighlightType.Move);

            var tileIndexAtt = GetTileIndex(tileX, tileY, HexagonDirection.DownRight, range + 1);
            if (HasTile(tileIndexAtt.Item1, tileIndexAtt.Item2))
            {
                var tileAtt = tiles[tileIndexAtt.Item1, tileIndexAtt.Item2];
                tileAtt.SetHighlight(HighlightType.Attack);
            }
            tile.Operation = CreateOperation(tileIndex2.Item1, tileIndex2.Item2, tileIndexAtt.Item1, tileIndexAtt.Item2);
        }

        //DownLeft:
        var tileIndex3 = GetTileIndex(tileX, tileY, HexagonDirection.DownLeft, range);

        if (HasTile(tileIndex3.Item1, tileIndex3.Item2))
        {
            var tile = tiles[tileIndex3.Item1, tileIndex3.Item2];
            tile.SetHighlight(HighlightType.Move);

            var tileIndexAtt = GetTileIndex(tileX, tileY, HexagonDirection.DownLeft, range + 1);
            if (HasTile(tileIndexAtt.Item1, tileIndexAtt.Item2))
            {
                var tileAtt = tiles[tileIndexAtt.Item1, tileIndexAtt.Item2];
                tileAtt.SetHighlight(HighlightType.Attack);
            }
            tile.Operation = CreateOperation(tileIndex3.Item1, tileIndex3.Item2, tileIndexAtt.Item1, tileIndexAtt.Item2);
        }

        //Right:
        var tileIndex4 = GetTileIndex(tileX, tileY, HexagonDirection.Right, range);

        if (HasTile(tileIndex4.Item1, tileIndex4.Item2))
        {
            var tile = tiles[tileIndex4.Item1, tileIndex4.Item2];
            tile.SetHighlight(HighlightType.Move);

            var tileIndexAtt = GetTileIndex(tileX, tileY, HexagonDirection.Right, range + 1);
            if (HasTile(tileIndexAtt.Item1, tileIndexAtt.Item2))
            {
                var tileAtt = tiles[tileIndexAtt.Item1, tileIndexAtt.Item2];
                tileAtt.SetHighlight(HighlightType.Attack);
            }
            tile.Operation = CreateOperation(tileIndex4.Item1, tileIndex4.Item2, tileIndexAtt.Item1, tileIndexAtt.Item2);
        }

        //Left:
        var tileIndex5 = GetTileIndex(tileX, tileY, HexagonDirection.Left, range);

        if (HasTile(tileIndex5.Item1, tileIndex5.Item2))
        {
            var tile = tiles[tileIndex5.Item1, tileIndex5.Item2];
            tile.SetHighlight(HighlightType.Move);

            var tileIndexAtt = GetTileIndex(tileX, tileY, HexagonDirection.Left, range + 1);
            if (HasTile(tileIndexAtt.Item1, tileIndexAtt.Item2))
            {
                var tileAtt = tiles[tileIndexAtt.Item1, tileIndexAtt.Item2];
                tileAtt.SetHighlight(HighlightType.Attack);
            }
            tile.Operation = CreateOperation(tileIndex5.Item1, tileIndex5.Item2, tileIndexAtt.Item1, tileIndexAtt.Item2);
        }
    }
}