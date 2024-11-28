using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void ExitGame() { Application.Quit(); }
    
    public void NavigateMenu(string s) { foreach(Transform child in transform) child.gameObject.SetActive(child.gameObject.name == s); }

}
