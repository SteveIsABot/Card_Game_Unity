using UnityEngine;

public class Pile : MonoBehaviour
{
    [SerializeField] public GameObject display;
    void Start() {}

    public void updateDisplay(string s, int v) {
        display.GetComponent<Card>().setSuit(s);
        display.GetComponent<Card>().setValue(v);
        display.GetComponent<Card>().updateMat();
        display.SetActive(true);
    }
}
