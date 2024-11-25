using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<GameObject> cards = new List<GameObject>();
    [SerializeField] public float sizeToFill;
    // Update is called once per frame
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
}
