using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class Tile : MonoBehaviour
{
    public int x;
    public int y;
	public GameObject moveHightligt;
	public GameObject attackHightligt;

    public GameStateController gameStateController;

    public delegate Task TileOperation(Piece piece);

    public TileOperation Operation;

	public IEnumerator AnimateAndDestroy()
	{
		GetComponent<Animator>().SetTrigger("Destroy");

		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}

    public void SetHighlight(HighlightType type)
	{
		moveHightligt.SetActive(type == HighlightType.Move);
		attackHightligt.SetActive(type == HighlightType.Attack);
    }
}

public enum HighlightType
{
	None, Move, Attack
}
