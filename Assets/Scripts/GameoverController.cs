using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameoverController : MonoBehaviour {

    public void goToMenu() {
        SceneManager.LoadScene("Menu");
    }
}
