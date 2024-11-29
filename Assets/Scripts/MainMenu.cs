using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start() {
        switch (CrossScene.exitGameState) {
            case GameState.Win: NavigateMenu("WinScreen"); break;
            case GameState.Lose: NavigateMenu("LoseScreen"); break;
            default: NavigateMenu("MainMenu"); break;
        }
    }

    public void ExitGame() { Application.Quit(); }
    
    public void NavigateMenu(string s) { foreach(Transform child in transform) child.gameObject.SetActive(child.gameObject.name == s); }

    public void PlayGame(bool jokers) {
        CrossScene.initWithJokers = jokers;
        SceneManager.LoadScene("GameScene");
    }

}
