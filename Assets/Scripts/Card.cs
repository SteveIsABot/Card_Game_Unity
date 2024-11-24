using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    [SerializeField] private string suit;
    [SerializeField] private int value;
    private Material cardMat;
    
    void Start()
    {
        if(suit == "Back") { cardMat = Resources.Load("Materials/Back_Card", typeof(Material)) as Material; }
        GetComponent<Renderer>().material = cardMat;
    }
    void Update()
    {
        
    }
}
