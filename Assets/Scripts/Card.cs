using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    [SerializeField] private string suit;
    [SerializeField] private int value;
    private Material cardMat;
    void Start() {
        updateMat();
    }

    public void updateMat() {
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

    public void setSuit(string s) { suit = s; }
    public void setValue(int v) {value = v; }
    public string getSuit() { return suit; }
    public int getValue() { return value; }
}
