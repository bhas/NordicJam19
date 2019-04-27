using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private void Log(string message)
    {
        // Debug.Log(message);
    }

    private void HandleHit(RaycastHit hitInfo)
    {
        var hitObject = hitInfo.transform.gameObject;
        if (hitObject.tag == "Tile")
        {
            Log("We hit a tile!");
            var tile = hitObject.GetComponentInParent<Tile>();
            tile.SetHighlight(HighlightType.Move);
        }

        if (hitObject.tag == "Piece")
        {
            Log("We hit a piece!");
            Destroy(hitObject.transform.parent.gameObject);
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
