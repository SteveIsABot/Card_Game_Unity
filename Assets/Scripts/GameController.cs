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

public class GameController : MonoBehaviour
{
    [SerializeField] public GameObject Deck;
    [SerializeField] public GameObject Player;
    [SerializeField] public GameObject Bot;
    [SerializeField] public GameObject Pile;
    [SerializeField] public GameObject TextObj;
    private List<cardData> playerHand = new List<cardData>();
    private List<cardData> botHand = new List<cardData>();
    private Stack<cardData> pileCards = new Stack<cardData>();

    void Start() {
        Deck = GameObject.Find("Deck");
        Pile = GameObject.Find("Pile");
        Player = GameObject.Find("PlayerHand");
        Bot = GameObject.Find("BotHand");
        TextObj = GameObject.Find("TurnText");

        Random.InitState((int)System.DateTime.Now.Ticks);
        Deck.GetComponent<Deck>().initDeck(false);

        for(int i = 0; i < 7; i++) {
            PlayerDraw();
            BotDraw();
        }

        deckToPile();

        if(Random.Range(1, 100) % 2 == 1) {
            PlayersTurn();
        } else {
            StartCoroutine(BotsTurn());
        }
    }

    void PlayerDraw() {
        cardData topCard = Deck.GetComponent<Deck>().drawTopCard();
        playerHand.Add(topCard);
        Player.GetComponent<Hand>().addCard(topCard.getSuit(), topCard.getValue(), true);
    }

    void BotDraw() {
        cardData topCard = Deck.GetComponent<Deck>().drawTopCard();
        botHand.Add(topCard);
        Bot.GetComponent<Hand>().addCard("Back", 0, false);
    }

    void deckToPile() {
        pileCards.Push(Deck.GetComponent<Deck>().drawTopCard());
        cardData topCard = pileCards.Peek();
        Pile.GetComponent<Pile>().updateDisplay(topCard.getSuit(), topCard.getValue());
    }

    void PlayersTurn() {
        TextObj.GetComponent<TextMeshPro>().text = " \nPlayer Turn\n↓";
        Debug.Log("Players Turn");
    }

    IEnumerator BotsTurn() {
        
        TextObj.GetComponent<TextMeshPro>().text = "↑\nOpponent Turn\n ";

        yield return new WaitForSeconds(3f);

        cardData topPileCard = pileCards.Peek();
        bool playedCard = false;

        for(int i = 0; i < botHand.Count; i++) {
            
            if(botHand[i].getSuit() == topPileCard.getSuit() || botHand[i].getValue() == topPileCard.getValue() ||
               botHand[i].getSuit() == "Red" || botHand[i].getSuit() == "Black") {
                
                Bot.GetComponent<Hand>().removeCard("Back", 0);
                Pile.GetComponent<Pile>().updateDisplay(botHand[i].getSuit(), botHand[i].getValue());

                pileCards.Push(botHand[i]);
                botHand.RemoveAt(i);
                playedCard = true;
                break;
            }
        }

        if(!playedCard) BotDraw();
        
        if(botHand.Count <= 0) {
            Debug.Log("You Lose");
        } else {
            PlayersTurn();
        }
    }
}
