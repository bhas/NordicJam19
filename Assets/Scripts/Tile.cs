using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public Tuple<int, int> PositionIndex;
	public GameObject moveHightligt;
	public GameObject attackHightligt;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SetHighlight(HighlightType type)
	{
		moveHightligt.SetActive(type == HighlightType.Move);
		attackHightligt.SetActive(type == HighlightType.Attack);
	}

	public void OnMouseDown()
	{
		Destroy(this.gameObject);
	}
}

public enum HighlightType
{
	None, Move, Attack
}
