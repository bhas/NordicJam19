using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTester : MonoBehaviour
{
	public List<TileData> tiles;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var tile in tiles)
		{
			tile.tile.SetHighlight(tile.highlight);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class TileData
{
	public Tile tile;
	public HighlightType highlight;
}
