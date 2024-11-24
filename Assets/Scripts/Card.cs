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
        string matName = "";
        
        matName += suit switch {
            "♥" => "H",
            "♦" => "D",
            "♣" => "C",
            "♠" => "S",
            "Red" => "Red",
            "Black" => "Black",
            _ => "Back",
        };

        matName += "_";

        matName += value switch {
            0 => "Joker",
            1 => "A",
            11 => "J",
            12 => "Q",
            13 => "K",
            _ => value.ToString(),
        };
        
        cardMat = suit == "Back" ? Resources.Load("Materials/Back_Card", typeof(Material)) as Material : Resources.Load("Materials/" + matName, typeof(Material)) as Material;
        GetComponent<Renderer>().material = cardMat;
    }
    void Update()
    {
        
    }
}
