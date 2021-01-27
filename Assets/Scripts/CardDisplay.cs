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

    [Header("Card Status")]
    public Sprite cardBack;


    //system vars
    Sprite cardFace;
    RectTransform rectT;
    Vector3 increasedSize;
    Vector3 originalSize;
    string tToolTip;
    bool faceDown = false;

    /// <summary>
    /// START --- GENERATES ASSIGNS VALUES TO DISPLAY
    /// </summary>
    void Start()
    {
        //asigning system variables
        cardFace = GetComponent<Image>().sprite;
        rectT = gameObject.GetComponent<RectTransform>();
        increasedSize = new Vector3(1.2f, 1.2f, 1.2f);
        originalSize = rectT.localScale;


        //randomly generates card values
        GenCardVals();

        if (card == null)
        {
            Debug.LogError("CARD HAS NO ASSIGNED CARD CLASS. EXITING APPLICATION");
            Application.Quit();
            return;
        }

        //assigning public vars in accordance with generated values 
        upperVal.text = card.cardValue;
        upperSymbl.sprite = card.cardType;

        lowerVal.text = card.cardValue;
        lowerSymbl.sprite = card.cardType;

        AssignToolTip();
        tooltip.gameObject.SetActive(false);

        cardCenter.text = card.cardValue + card.cardType.name[0].ToString();
        FlipSet(faceDown);
    }

    /// <summary>
    /// RANDOMLY GENERATES CARD VALUES
    /// </summary>
    void GenCardVals()
    {
        //assigning card number
        card.cardValue = Random.Range(1, 14).ToString();

        //assigns card Aces, Jacks, Queens and Kings.
        switch (card.cardValue)
        {
            case "1":
            case "14":
                card.cardValue = "A";
                break;
            case "11":
                card.cardValue = "J";
                break;
            case "12":
                card.cardValue = "Q";
                break;
            case "13":
                card.cardValue = "K";
                break;
            default:
                break;
        }

        //assigning card suit
        //only does if the card's scriptable Object suit array slot is empty
        // if it is empty then the card will have a predetermined suit rather than a random one
        if (card.allCardTypes.Length > 0)
            card.cardType = card.allCardTypes[Random.Range(0, card.allCardTypes.Length)];
    }

    /// <summary>
    /// ASSIGNS "TOOLTIP" VAR BASED ON VALUE, INCLUDING ACE, JAKC, QUEEN AND KING
    /// </summary>
    void AssignToolTip()
    {
        switch (card.cardValue)
        {
            case "A":
                tooltip.text = "Ace of " + card.cardType.name;
                break;

            case "J":
                tooltip.text = "Jack of " + card.cardType.name;
                break;

            case "Q":
                tooltip.text = "Queen of " + card.cardType.name;
                break;

            case "K":
                tooltip.text = "King of " + card.cardType.name;
                break;

            default:
                tooltip.text = card.cardValue + " of " + card.cardType.name;
                break;
        }
    }

    /// <summary>
    /// UPDATE --- SELECTS CARDS, 
    /// </summary>
    private void Update()
    {
        if (tooltip.gameObject.activeSelf)
        {
            if (!faceDown)
            {
                SelectCard();
            }

            if (Input.GetMouseButtonDown(1))
                FlipSet(faceDown);
        }
    }

    void FlipSet(bool BOOL)
    {
        if (cardCenter.gameObject.activeSelf == !BOOL)
        {
            cardCenter.gameObject.SetActive(BOOL);
            upperVal.gameObject.SetActive(BOOL);
            upperSymbl.gameObject.SetActive(BOOL);
            lowerVal.gameObject.SetActive(BOOL);
            lowerSymbl.gameObject.SetActive(BOOL);
            if (BOOL)
            {
                GetComponent<Image>().sprite = cardFace;
                tooltip.text = tToolTip;
            }
            else
            {
                GetComponent<Image>().sprite = cardBack;
                tToolTip = tooltip.text; 
                tooltip.text = "???";
            }
            faceDown = !faceDown;
        }
    }

    void SelectCard()
    {
        //increases card size if it is clicked on
        //card is "selected"GenCardVals()
        // if card is already selected, unselects
        if (tooltip.gameObject.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (rectT.localScale == originalSize)
                    rectT.localScale = increasedSize;
                else
                    rectT.localScale = originalSize;
            }
        }
        //decreases size when elsewhere is clicked
        //card is "unselected"
        else
        {
            if (Input.GetMouseButtonDown(0) && rectT.localScale == increasedSize)
                rectT.localScale = originalSize;
        }
    }
}
