using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour, IPointerClickHandler
{
    public Card card;

    public Text moveAmount;
    public Text attackAmount;
    public Text flavorText;

    void Start()
    {
        moveAmount.text = card.moveAmount;
        attackAmount.text = card.attackAmount;
        // flavorText.text = card.flavorText;
    }

    void Select()
    {
        GameStateController.GetInstance().CardSelected(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == 0)
            Select();
    }
}
