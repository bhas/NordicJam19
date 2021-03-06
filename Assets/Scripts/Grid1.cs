﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid1 : MonoBehaviour
{
    public TextAsset mapFile;

    public Transform hexOrefab;

    public Transform piece1fab;
    public Transform piece2fab;

    public bool[,] mapLayOutInt;
	public Tile[,] Tiles;

	public static float hexwidth = 0.87f;
    static float hexheight = 1f;

    public static float gap = 0.05f;

    public GameStateController gameStateController;

    int gridWidth;
    int gridHeight;
    static Vector3 startPos;

    void Start()
    {
        ReadMapFile();
        AddGap();
        CalcStartPos();
		CreateGrid();
        CreatePieces();
    }

    private Piece CreatePiece(string name, Transform prefab, int x, int y)
    {
        Transform piece = (Transform)Instantiate(prefab);
        Vector2 gridPos = new Vector2(x, y);
        piece.position = CalcWorldPos(gridPos);
        piece.parent = this.transform;
        piece.name = name;
        var pieceScript = piece.GetComponent<Piece>();
        pieceScript.x = x;
        pieceScript.y = y;
        return pieceScript;
    }

    void CreatePieces()
    {
        gameStateController.piece1 = CreatePiece("Piece1", piece1fab, 4, 5);
        gameStateController.piece2 = CreatePiece("Piece2", piece2fab, 2, 2);
    }

    void ReadMapFile()
    {
        string[] mapLayOut = mapFile.text.Split('\n');
        mapLayOutInt = new bool[mapLayOut.Length,mapLayOut[0].Length];
		Tiles = new Tile[mapLayOut.Length, mapLayOut[0].Length];

		for (int ii = 0; ii < mapLayOut.Length; ii++)
        {
            for(int iii = 0; iii < mapLayOut[ii].Length; iii++)
            {
                string fuck = mapLayOut[ii].Substring(iii, 1);
                mapLayOutInt[ii, iii] = fuck == "1";
            }   
        }
        gridWidth = mapLayOut[0].Length - 1;
        gridWidth = mapLayOut[0].Length;
        gridHeight = mapLayOut.Length;

    }

    void AddGap()
    {
        hexwidth += hexwidth * gap;
        hexheight += hexheight * gap;
    }

    void CalcStartPos()
    {
        float offset = 0;
        if (gridHeight/2 % 2 != 0)
        {
            offset = hexwidth / 2;
        }
        float x = -hexwidth * gridWidth / 2 - offset;
        float z = hexheight * 0.75f * gridHeight / 2;

        startPos = new Vector3(x, 0, z);
    }

    public static Vector3 CalcWorldPos(Vector3 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
        {
            offset = hexwidth / 2;
        }
        float x = startPos.x + gridPos.x * hexwidth + offset;
        float z = startPos.z + gridPos.y * hexheight * 0.75f;

        return new Vector3(x, 0, z);
    }
    void CreateGrid()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth-1; x++)
            {
                if (mapLayOutInt[x, y])
                {
					StartCoroutine(CreateTileAsync(x, y));
				}
                else
                {
                    print("get the fuck out");
                }
				
            }
        }
        gameStateController.tiles = Tiles;
    }

	private IEnumerator CreateTileAsync(int x, int y)
	{
		float seconds = x * 0.07f + y * 0.16f;
		yield return new WaitForSeconds(seconds);

		Transform hex = Instantiate(hexOrefab) as Transform;
		Vector2 gridPos = new Vector2(x, y);
		hex.position = CalcWorldPos(gridPos);
		hex.parent = this.transform;
		hex.name = "Hexagon" + x + "|" + y;
		var tileScript = hex.GetComponent<Tile>();
		tileScript.x = x;
		tileScript.y = y;
		tileScript.gameStateController = gameStateController;
		Tiles[x, y] = tileScript;
	}
}