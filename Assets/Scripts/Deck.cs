using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    
    private List<cardData> cards = new List<cardData>();
    [SerializeField] public GameObject displayCard;
    [SerializeField] public TextMeshPro text;

    public void initDeck(bool initJokers) {
        for(int i = 0; i < 4; i++) {

            string s = "";
            s = i switch
            {
                0 => "♥",
                1 => "♠",
                2 => "♦",
                _ => "♣",
            };

            for (int j = 1; j <= 13; j++) {
                cards.Add(new cardData(s, j));
            }
        }

        if(initJokers){
            cards.Add(new cardData("Black", 0));
            cards.Add(new cardData("Red", 0));
        }

        Shuffle();
        
        displayCard.GetComponent<Card>().owner = CardOwner.Decks;
    }

    void Update() {
        bool condition = cards.Count > 0;

        displayCard.SetActive(condition);
        text.text = "Remaining:\n" + cards.Count;
    }

    void Shuffle() {

        if(cards.Count < 2) return;
        int swapAmount = cards.Count + Random.Range(0, 10);

        for(int i = 0; i < swapAmount; i++) {
            int indexA = Random.Range(0, cards.Count);
            int indexB = Random.Range(0, cards.Count);
            cardData temp = cards[indexA];
            cards[indexA] = cards[indexB];
            cards[indexB] = temp;
        }
    }

    public cardData drawTopCard() {
        cardData topCard = cards[0];
        cards.RemoveAt(0);
        return topCard;
    }

    public void Restock(Stack<cardData> pileCards) {
        
        cardData topCard = pileCards.Pop();
        while(pileCards.Count > 0) cards.Add(pileCards.Pop());
        Shuffle();

        pileCards.Push(topCard);
    }

    public int sizeOfDeck() { return cards.Count; }
}
