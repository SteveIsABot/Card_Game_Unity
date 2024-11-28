using System.Collections;
using System.Collections.Generic;
using TMPro;
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
public enum CardOwner { Players, Bots, Decks, Piles }

public class GameController : MonoBehaviour
{
    [SerializeField] public GameObject Deck;
    [SerializeField] public GameObject Player;
    [SerializeField] public GameObject Bot;
    [SerializeField] public GameObject Pile;
    [SerializeField] public GameObject TextObj;
    [SerializeField] public bool playWithJokers = false;
    private List<cardData> playerHand = new List<cardData>();
    private List<cardData> botHand = new List<cardData>();
    private Stack<cardData> pileCards = new Stack<cardData>();
    private GameState gameState;

    void Start() {
        gameState = GameState.Start;
        Deck = GameObject.Find("Deck");
        Pile = GameObject.Find("Pile");
        Player = GameObject.Find("PlayerHand");
        Bot = GameObject.Find("BotHand");
        TextObj = GameObject.Find("TurnText");

        Random.InitState((int)System.DateTime.Now.Ticks);
        Deck.GetComponent<Deck>().initDeck(playWithJokers);

        for(int i = 0; i < 7; i++) {
            PlayerDraw();
            BotDraw();
        }

        deckToPile();

        if(Random.Range(1, 100) % 2 == 1) {
            gameState = GameState.PlayersTurn;
            PlayersTurn();
        } else {
            gameState = GameState.BotsTurn;
            StartCoroutine(BotsTurn());
        }
    }

    void PlayerDraw() {
        cardData topCard = Deck.GetComponent<Deck>().drawTopCard();
        playerHand.Add(topCard);
        Player.GetComponent<Hand>().addCard(topCard.getSuit(), topCard.getValue(), CardOwner.Players);
    }

    void BotDraw() {
        cardData topCard = Deck.GetComponent<Deck>().drawTopCard();
        botHand.Add(topCard);
        Bot.GetComponent<Hand>().addCard("Back", 0, CardOwner.Bots);
    }

    void deckToPile() {
        pileCards.Push(Deck.GetComponent<Deck>().drawTopCard());
        cardData topCard = pileCards.Peek();
        Pile.GetComponent<Pile>().updateDisplay(topCard.getSuit(), topCard.getValue());
    }

    void PlayersTurn() {
        if(gameState != GameState.PlayersTurn) return;
        if(Deck.GetComponent<Deck>().sizeOfDeck() < 1) Deck.GetComponent<Deck>().Restock(pileCards);
        TextObj.GetComponent<TextMeshPro>().text = " \nPlayer Turn\n↓";
    }

    IEnumerator BotsTurn() {
        
        if(Deck.GetComponent<Deck>().sizeOfDeck() < 1) Deck.GetComponent<Deck>().Restock(pileCards);
        TextObj.GetComponent<TextMeshPro>().text = "↑\nOpponent Turn\n ";

        yield return new WaitForSeconds(3f);

        cardData topPileCard = pileCards.Peek();
        bool playedCard = false;

        for(int i = 0; i < botHand.Count; i++) {
            
            if(topPileCard.getValue() == 0 || botHand[i].getValue() == 0 ||
               botHand[i].getSuit() == topPileCard.getSuit() || botHand[i].getValue() == topPileCard.getValue()) {
                
                Bot.GetComponent<Hand>().removeCard("Back", 0);
                Pile.GetComponent<Pile>().updateDisplay(botHand[i].getSuit(), botHand[i].getValue());

                if(botHand[i].getValue() == 2) for(int j = 0; j < 2; j++) PlayerDraw();
                if(botHand[i].getValue() == 1 && botHand[i].getSuit() == "♥") for(int j = 0; j < 5; j++) PlayerDraw();

                pileCards.Push(botHand[i]);
                botHand.RemoveAt(i);
                playedCard = true;
                break;
            }
        }

        if(!playedCard) BotDraw();
        
        if(botHand.Count <= 0) {
            gameState = GameState.Lose;
            Debug.Log("You Lose");
        } else if(playedCard && pileCards.Peek().getValue() == 8) {
            gameState = GameState.BotsTurn;
            StartCoroutine(BotsTurn());
        } else {
            gameState = GameState.PlayersTurn;
            PlayersTurn();
        }
    }

    public void PlayerCardClicked(string s, int v) {

        if(gameState != GameState.PlayersTurn) return;

        cardData topCard = pileCards.Peek();
        
        if(topCard.getValue() == 0 || v == 0 ||
           topCard.getSuit() == s || topCard.getValue() == v) {

                cardData cData = new cardData(s, v);
                Player.GetComponent<Hand>().removeCard(s, v);
                Pile.GetComponent<Pile>().updateDisplay(s, v);

                pileCards.Push(cData);
                playerHand.Remove(cData);

                if(v == 2) for(int j = 0; j < 2; j++) BotDraw();
                if(v == 1 && s == "♥") for(int j = 0; j < 5; j++) BotDraw();

                if(playerHand.Count <= 0) {
                    gameState = GameState.Win;
                    Debug.Log("You Win");
                } else if (v == 8) {
                    gameState = GameState.PlayersTurn;
                    PlayersTurn();
                } else {
                    gameState = GameState.BotsTurn;
                    StartCoroutine(BotsTurn());
                }
        }
    }

    public void PlayerClickedDeck() {

        if(gameState != GameState.PlayersTurn) return;
        PlayerDraw();

        gameState = GameState.BotsTurn;
        StartCoroutine(BotsTurn());
    }
}
