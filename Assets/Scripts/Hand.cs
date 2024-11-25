using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] public GameObject cardPrefab;
    [SerializeField] private List<GameObject> cards = new List<GameObject>();
    [SerializeField] public float sizeToFill;

    void Update()
    {
        float startPoint = (-sizeToFill + 0.5f) / 2;
        float placeAmount = sizeToFill / cards.Count;

        Vector3 newPos = new Vector3(startPoint, 0.2f, transform.position.z + 0.1f);

        for(int i = 0; i < cards.Count; i++) {
            cards[i].transform.position = newPos;
            newPos.x += placeAmount;
            newPos.y += 0.01f;
        }
    }

    public void addCard(string s, int v)
    {
        GameObject newCard = Instantiate(cardPrefab, transform);
        newCard.GetComponent<Card>().setSuit(s);
        newCard.GetComponent<Card>().setValue(v);
        newCard.GetComponent<Card>().updateMat();
        cards.Add(newCard);
    }
}
