using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [Header("Game Controllers")]
    public int amountOfCards = 8;

    [Header("Cards")]
    public GameObject cardPrefab;
    public Sprite[] clubs;

    //system vars
    GameObject[] allCards;
    GameObject[] cardPoints;
    string[] allCardVals;

    void Start()
    {
        //Prevents an odd amount of cards from being input
        if (amountOfCards % 2 != 0)
        {
            Debug.LogError("CANNOT PLAY MATCH WITH AN ODD AMOUNT OF CARDS");
            Application.Quit();
            return;
        }

        //setting system vars
        allCards = new GameObject[amountOfCards];
        cardPoints = GameObject.FindGameObjectsWithTag(amountOfCards + "CardSpawns");
        allCardVals = new string[amountOfCards / 2];

        for (int i = 0; i < amountOfCards; i += 2)
        {
            string x = "";
            int y = 0;
            bool loopBreak = false;
            while (!loopBreak)
            {
                x = Random.Range(1, 14).ToString();
                y = Random.Range(1, 4);
                string xy = x + y.ToString();
                loopBreak = GenerateValues(xy, i);
            }

            MakeCard(i, x, y);
            MakeCard(i + 1, x, y); //generates identical card in the next slot of the array
        }

        for (int i = 0; i < amountOfCards; i++)
        {
            Debug.Log(allCards[i].GetComponent<CardDisplay>().cardCenter.text + ", " + i);
        }
    }

    bool GenerateValues(string cardVal, int i)
    {
        for (int v = 0; v < allCardVals.Length; v++)
        {
            //Debug.Log(allCardVals[v]);
            if (allCardVals[v] == cardVal)
            {
                return false;
            }
        }

        //Debug.Log(allCardVals[i / 2]);
        allCardVals[i / 2] = cardVal;
        return true;
    }

    void MakeCard(int i, string x, int y)
    {
        int cardPos = -1;
        while (cardPos == -1)
        {
            cardPos = SetCardPos();
        }
        allCards[i] = Instantiate(cardPrefab, cardPoints[cardPos].transform);
        allCards[i].GetComponent<CardDisplay>().card.CardConstructor(x, clubs[y]);
        allCards[i].GetComponent<CardDisplay>().SetValsOnStart();
        //Debug.Log(allCards[i].GetComponent<CardDisplay>().upperVal);
    }

    int SetCardPos()
    {
        int pos = Random.Range(0, cardPoints.Length);
        if (cardPoints[pos].transform.childCount > 0)
            return -1;
        else
            return pos;
    }
}
