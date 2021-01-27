using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [Header ("Game Controllers")]
    public int amountOfCards = 8;

    [Header("Cards")]
    public GameObject cardPrefab;
    public Sprite[] clubs;

    //system vars
    GameObject[] allCards;

    void Start()
    {
        //Prevents an odd amount of cards from being input
        if (amountOfCards % 2 != 0)
        {
            Debug.LogError("CANNOT PLAY MATCH WITH AN ODD AMOUNT OF CARDS");
            Application.Quit();
            return;
        }

        allCards = new GameObject[amountOfCards];

        for (int i = 0; i < amountOfCards/2; i++)
        {
            string x = Random.Range(1, 14).ToString();
            int y = Random.Range(1, 4);
            allCards[i] = Instantiate(cardPrefab);
            allCards[i].GetComponent<CardDisplay>().card.CardConstructor(x, clubs[y]);
            Debug.Log(allCards[i].GetComponent<CardDisplay>().card.cardValue);
            allCards[i + 1] = Instantiate(cardPrefab);
            allCards[i + 1].GetComponent<CardDisplay>().card.CardConstructor(x, clubs[y]);
            Debug.Log(allCards[i+1].GetComponent<CardDisplay>().card.cardValue);
        }
    }

    void Update()
    {
        
    }
}
