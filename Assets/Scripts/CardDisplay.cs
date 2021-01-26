using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{

    [Header ("CardType")]
    
    public PlayingCard card;

    [Header ("Card Icons")]

    public Text tooltip;

    public Text upperVal;
    public Image upperSymbl;

    public Text lowerVal;
    public Image lowerSymbl;

    public Text cardCenter;

    void Start()
    {
        card.cardValue = Random.Range(1, 9);

        if (card.allCardTypes.Length > 0)
        {
            card.cardType = card.allCardTypes[Random.Range(0, card.allCardTypes.Length)];
        }

        if (card == null || card.cardValue >= 10 || card.cardValue <= 0)
        {
            Debug.LogError("CARD HAS NO ASSIGNED CARD CLASS OR CARD VALUE EXCEEDS 1-9 RANGE. EXITING APPLICATION");
            Application.Quit();
            return;
        }

        upperVal.text = card.cardValue.ToString();
        upperSymbl.sprite = card.cardType;

        lowerVal.text = card.cardValue.ToString();
        lowerSymbl.sprite = card.cardType;

        tooltip.text = card.cardValue + " of " + card.cardType.name;
        tooltip.gameObject.SetActive(false);

        cardCenter.text = card.cardValue + card.cardType.name[0].ToString();
    }
}
