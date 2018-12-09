using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour {


    public void startGame() {
        SceneManager.LoadScene("StartCinematic");
    }

    public void loadGame() {
        int levelLastTimePlayed = PlayerPrefs.GetInt("Level", 0);

        if(levelLastTimePlayed == 1) {
            SceneManager.LoadScene("Level1");
        }else if(levelLastTimePlayed == 2) {
            SceneManager.LoadScene("Level2Boss");
        }else if(levelLastTimePlayed == 3) {
            SceneManager.LoadScene("Level2Boss");
        }
        
    }

    public void optionsUI() {
        SceneManager.LoadScene("Options");
    }

    public void quitGame() {
        Application.Quit();
    }

}
