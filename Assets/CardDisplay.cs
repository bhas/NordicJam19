using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Text moveAmount;
    public Text attackAmount;
    public Text flavorText;

    void Start()
    {
        moveAmount.text = card.moveAmount;
        attackAmount.text = card.attackAmount;
        flavorText.text = card.flavorText;
    }
}
