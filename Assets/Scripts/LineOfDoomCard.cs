using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfDoomCard : MonoBehaviour, ICardOperation
{
    public void HighlightMoveOptions(int range, int tileX, int tileY)
    {

    }

    bool HasEmptyTile(int x, int y)
    {
        var gameController = GameStateController.GetInstance();
        return gameController.HasEmptyTile(x, y);
    }
}
