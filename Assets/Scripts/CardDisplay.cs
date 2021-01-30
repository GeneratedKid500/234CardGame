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

    [Header("System")]
    public AudioSource audioSource;

    //system vars
    MatchGameManager gameManager;
    Sprite cardFace;
    RectTransform rectT;
    Vector3 increasedSize;
    Vector3 originalSize;
    string tToolTip;
    float timer;
    bool faceDown = false;
    bool isEnabled = true;
    [HideInInspector] public bool matched = false;
    [HideInInspector] public bool disableAll = false;

    /// <summary>
    /// START --- GENERATES ASSIGNS VALUES TO DISPLAY
    /// </summary>
    void Start()
    {
        //asigning system variables
        gameManager = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<MatchGameManager>();
        cardFace = GetComponent<Image>().sprite;
        rectT = gameObject.GetComponent<RectTransform>();
        increasedSize = new Vector3(1.2f, 1.2f, 1.2f);
        originalSize = rectT.localScale;

        lowerVal.text = upperVal.text;
        lowerSymbl.sprite = upperSymbl.sprite;

        //AssignToolTip();
        tooltip.gameObject.SetActive(false);
    }

    public void SetValsOnStart()
    {
        AssignCardJQK();
        upperVal.text = card.cardValue;
        upperSymbl.sprite = card.cardType;
        cardCenter.text = card.cardValue + card.cardType.name[0].ToString();
        AssignToolTip();
    }

    /// <summary>
    /// RANDOMLY GENERATES CARD VALUES
    /// </summary>
    void AssignCardJQK()
    {
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
    }

    /// <summary>
    /// ASSIGNS "TOOLTIP" VAR BASED ON VALUE, INCLUDING ACE, JAKC, QUEEN AND KING
    /// </summary>
    void AssignToolTip()
    {
        switch (upperVal.text)
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
        if (disableAll || matched)
        {
            if (isEnabled)
                isEnabled = false;
        }
        else
        {
            if (!isEnabled)
                isEnabled = true;
        }

        if (timer < 1)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                FlipSet(faceDown);
            }
        }
        else
        {
            //if mouse is hovering over card
            if (tooltip.gameObject.activeSelf)
            {
                //if card is face up
                if (!faceDown)
                {
                    //SelectCard();
                }

                if (Input.GetMouseButtonDown(0) && isEnabled)
                {
                    FlipSet(faceDown);
                }
            }
        }
    }

    //flips the position of the card
    //toggles the active state of the children (which display the information)
    void FlipSet(bool BOOL)
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
            audioSource.Play();
            faceDown = !faceDown;

            if (!faceDown)
            {
                gameManager.CardFlipped(this);
            }
            else
            {
                gameManager.UnselectCard(this);
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

    public void publicFlip()
    {
        FlipSet(faceDown);
    }
}
