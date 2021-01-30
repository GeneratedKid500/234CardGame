using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchGameManager : MonoBehaviour
{
    //editor
    public AudioSource audioSource;
    public AudioClip correct;
    public AudioClip incorrect;

    //system vars
    CardDisplay card1;
    CardDisplay card2;
    public static int score = 0;
    float timer = 0;
    bool isTimer;
    bool ontoCard2 = false;

    private void Update()
    {
        //picked cards do not match
        if (isTimer)
        {
            timer += Time.deltaTime;
        }
        if (timer > 0.75)
        {
            Debug.Log("Failed");
            card1.publicFlip();
            card2.publicFlip();
            timer = 0;
            isTimer = false;
            score--;
            ToggleCardsCanFlip();
        }
    }

    public void CardFlipped(CardDisplay card)
    {
        if (!ontoCard2)
        {
            card1 = card;
            ontoCard2 = true;
        }
        else
        {
            ToggleCardsCanFlip();
            card2 = card;
            if (card2.cardCenter.text == card1.cardCenter.text)
            {
                //picked cards do match
                audioSource.PlayOneShot(correct);
                Debug.Log("Success!");
                card1.matched = true;
                card.matched = true;
                score++;
                ToggleCardsCanFlip();
                CheckGameWin();
            }
            else
            {
                isTimer = true;
                audioSource.PlayOneShot(incorrect);
            }
            ontoCard2 = false;
        }
    }

    void ToggleCardsCanFlip()
    {
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
        for(int i = 0; i < cards.Length; i++)
        {
            cards[i].GetComponent<CardDisplay>().disableAll = !cards[i].GetComponent<CardDisplay>().disableAll;
        }
    }

    void CheckGameWin()
    {
        int number = 0;
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
        foreach (GameObject card in cards)
        {
            if (card.GetComponent<CardDisplay>().matched)
            {
                number++;
            }

        }
        if (number >= cards.Length)
        {
            Debug.Log("Game Won!");
            SceneManager.LoadScene("GameFinished");
        }
    }

    public void UnselectCard(CardDisplay card)
    {
        if (card = card1)
        {
            ontoCard2 = false;
        }
    }
}
