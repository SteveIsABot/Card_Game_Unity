using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

public enum GameState { Start, PlayersTurn, BotsTurn, Win, Lose }

public class GameController : MonoBehaviour
{
    [SerializeField] public GameObject Deck;
    [SerializeField] public GameObject Player;
    [SerializeField] public GameObject Bot;
    [SerializeField] public GameObject Pile;
    private List<cardData> playerHand = new List<cardData>();
    private List<cardData> botHand = new List<cardData>();
    private Queue<cardData> pileCards = new Queue<cardData>();
    private GameState state;

    void Start() {
        state = GameState.Start;
        Deck = GameObject.Find("Deck");
        Pile = GameObject.Find("Pile");
        Player = GameObject.Find("PlayerHand");
        Bot = GameObject.Find("BotHand");

        Random.InitState((int)System.DateTime.Now.Ticks);
        Deck.GetComponent<Deck>().initDeck(false);

        for(int i = 0; i < 7; i++) {
            PlayerDraw();
            BotDraw();
        }

        deckToPile();

        if(Random.Range(1, 100) % 2 == 1) {
            state = GameState.PlayersTurn;
            //PlayersTurn();
        } else {
            state = GameState.BotsTurn;
            //BotsTurn();
        }
    }

    void PlayerDraw() {
        cardData topCard = Deck.GetComponent<Deck>().drawTopCard();
        playerHand.Add(topCard);
        Player.GetComponent<Hand>().addCard(topCard.getSuit(), topCard.getValue());
    }

    void BotDraw() {
        cardData topCard = Deck.GetComponent<Deck>().drawTopCard();
        botHand.Add(topCard);
        Bot.GetComponent<Hand>().addCard("Back", 0);
    }

    void deckToPile() {
        pileCards.Enqueue(Deck.GetComponent<Deck>().drawTopCard());
        cardData topCard = pileCards.Peek();
        Pile.GetComponent<Pile>().updateDisplay(topCard.getSuit(), topCard.getValue());
    }

    void PlayersTurn() {
        if(state != GameState.PlayersTurn) return;
    }

    void BotsTurn() {
        if(state != GameState.BotsTurn) return;
    }
}
