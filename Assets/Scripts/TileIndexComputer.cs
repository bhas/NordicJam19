using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileIndexComputer
{
    public static Tuple<int, int> GetTileIndex(int posx, int posy, HexagonDirection dir, int range)
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
}
