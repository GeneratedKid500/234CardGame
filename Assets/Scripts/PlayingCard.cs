﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card")]
public class PlayingCard : ScriptableObject
{
    public string cardValue;

    public Sprite cardType;

    public Sprite[] allCardTypes;

    public void CardConstructor(string cardRank, Sprite cardClub)
    {
        cardValue = cardRank;
        cardType = cardClub;
    }
}
