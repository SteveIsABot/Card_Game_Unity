using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct cardData {
    public string suit; public int value;

    public cardData(string s, int v) {
        suit = s;
        value = v;
    }

    public string getSuit() { return suit; }
    public int getValue() { return value; }
}

public class GameController : MonoBehaviour
{
    [SerializeField] public GameObject Deck;
    [SerializeField] public GameObject Player;
    [SerializeField] public GameObject Bot;
    [SerializeField] public GameObject Pile;
    private List<cardData> playerHand = new List<cardData>();
    private List<cardData> botHand = new List<cardData>();
    private Queue<cardData> pileCards = new Queue<cardData>();

    //bool finised = false;

    void Start()
    {
        Deck = GameObject.Find("Deck");
        Pile = GameObject.Find("Pile");
        Player = GameObject.Find("PlayerHand");
        Bot = GameObject.Find("BotHand");

        Deck.GetComponent<Deck>().initDeck(false);
        for(int i = 0; i < 7; i++) {
            PlayerDraw();
            BotDraw();
        }

        deckToPile();
    }

    void Update()
    {
        
    }

    void PlayerDraw()
    {
        cardData topCard = Deck.GetComponent<Deck>().drawTopCard();
        playerHand.Add(topCard);
        Player.GetComponent<Hand>().addCard(topCard.getSuit(), topCard.getValue());
    }

    void BotDraw()
    {
        cardData topCard = Deck.GetComponent<Deck>().drawTopCard();
        botHand.Add(topCard);
        Bot.GetComponent<Hand>().addCard("Back", 0);
    }

    void deckToPile()
    {
        pileCards.Enqueue(Deck.GetComponent<Deck>().drawTopCard());
        cardData topCard = pileCards.Peek();
        Pile.GetComponent<Pile>().updateDisplay(topCard.getSuit(), topCard.getValue());
    }
}
