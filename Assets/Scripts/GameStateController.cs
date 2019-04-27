using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public Tile[] tiles;
    public Tile testTile1;
    public Tile testTile2;
    public Tile testTile3;
    public Tile testTile4;

    void Start()
    {
        testTile1.SetHighlight(HighlightType.Move);
        testTile2.SetHighlight(HighlightType.Attack);
    }

    public void CardSelected()
    {

    }
}
