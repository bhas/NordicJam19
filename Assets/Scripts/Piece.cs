using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int x;
    public int y;

    public void Move(int _x, int _y, Vector3 worldPosition)
    {
        x = _x;
        y = _y;
        transform.position = worldPosition;
    }
}
