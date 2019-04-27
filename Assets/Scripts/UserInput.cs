using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class UserInput : MonoBehaviour
{
    public GameStateController gameStateController;

    private void Log(string message)
    {
        // Debug.Log(message);
    }

    private Task HandleTileHit(Tile tile)
    {
        Log("We hit a tile!");
        return gameStateController.TileHit(tile);
    }

    private void HandlePieceHit(Piece piece)
    {
        Log("We hit a piece!");
        // piece.Select();
    }

    private void HandleHit(RaycastHit hitInfo)
    {
        var hitObject = hitInfo.transform.gameObject;
        if (hitObject.tag == "Tile")
            HandleTileHit(hitObject.GetComponentInParent<Tile>());
        else if (hitObject.tag == "Piece")
        {
            HandlePieceHit(hitObject.GetComponentInParent<Piece>());
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Log("Mouse clicked!");
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 10);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                HandleHit(hitInfo);
        }
    }
}
