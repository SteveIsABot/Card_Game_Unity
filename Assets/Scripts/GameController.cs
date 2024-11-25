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

    void Start()
    {
        Deck = GameObject.Find("Deck");
        Pile = GameObject.Find("Pile");
        Player = GameObject.Find("PlayerHand");
        Bot = GameObject.Find("BotHand");

        Deck.GetComponent<Deck>().initDeck(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
