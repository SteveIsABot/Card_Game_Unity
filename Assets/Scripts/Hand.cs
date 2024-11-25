using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private List<GameObject> cards = new List<GameObject>();
    [SerializeField] public float sizeToFill;
    public bool isBot = true;

    void Update()
    {
        float startPoint = (-sizeToFill + 0.5f) / 2;
        float placeAmount = sizeToFill / cards.Count;

        Vector3 newPos = new Vector3(startPoint, 0.2f, transform.position.z + 0.1f);

        for(int i = 0; i < cards.Count; i++) {
            
            if(isBot) {
                cards[i].GetComponent<Card>().setSuit("Back");
                cards[i].GetComponent<Card>().updateMat();
            }
            
            cards[i].transform.position = newPos;
            newPos.x += placeAmount;
            newPos.y += 0.01f;
        }
    }
}
