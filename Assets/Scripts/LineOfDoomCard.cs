using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfDoomCard : MonoBehaviour, ICardOperation
{
    public void HighlightMoveOptions(int range, int tileX, int tileY)
    {

    }

    bool HasTile(int x, int y)
    {
        var gameController = GameStateController.GetInstance();
        return gameController.HasTile(x, y);
    }
}
