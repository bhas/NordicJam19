using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string moveAmount;
    public string attackAmount;
    public string flavorText;
}
