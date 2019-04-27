using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int x;
    public int y;
    public Tile tile;

    public void Select()
    {
        tile.SetHighlight(HighlightType.Move);
    }
}
