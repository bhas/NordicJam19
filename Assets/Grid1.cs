﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid1 : MonoBehaviour
{
    public Transform hexOrefab;

    public int gridWidth = 11;
    public int gridHeight = 11;

    public float hexwidth = 1.1f;
    float hexheight = 1f;

    public float gap = 0.0f;

    Vector3 startPos;

    void Start()
    {
        AddGap();
        CalcStartPos();
        CreateGrid();
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

    Vector3 CalcWorldPos(Vector3 gridPos)
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
            for (int x = 0; x < gridWidth; x++)
            {
                Transform hex = Instantiate(hexOrefab) as Transform;
                Vector2 gridPos = new Vector2(x, y);
                hex.position = CalcWorldPos(gridPos);
                hex.parent = this.transform;
                hex.name = "Hexagon" + x + "|" + y;

            }
        }
    }

    }