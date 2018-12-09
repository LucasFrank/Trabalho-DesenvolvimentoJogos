using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour {

    void Start() {
    }

    public void startGame() {
        SceneManager.LoadScene("StartCinematic");
    }

    public void loadGame() {
        //Load last game
    }

    public void optionsUI() {
        SceneManager.LoadScene("Options");
    }

    public void quitGame() {
        Application.Quit();
    }

    public void returnMenu() {
        SceneManager.LoadScene("Menu");
    }
}
