using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pile : MonoBehaviour
{
    private Queue<GameObject> pileCards = new Queue<GameObject>();
    [SerializeField] public GameObject display;
    void Start() {}

    void Update()
    {
        if(pileCards.Count > 0) {
            Card topCard = pileCards.Peek().GetComponent<Card>();
            display.GetComponent<Card>().setSuit(topCard.getSuit());
            display.GetComponent<Card>().setValue(topCard.getValue());
            display.GetComponent<Card>().updateMat();
            display.SetActive(true);
        } else {
            display.SetActive(false);
        }
    }
}
