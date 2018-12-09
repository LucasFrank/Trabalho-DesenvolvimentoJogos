using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {

    public GameObject playerPrefab;
    private GameObject player;

    private Vector3 spawnPosition;

    private int currentLevel;
    private Scene currentScene;

    // Use this for initialization
    void Start () {
        this.spawnPosition = new Vector3(0, 1, 0);
        this.player = Instantiate<GameObject>(playerPrefab,spawnPosition, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
        currentScene = SceneManager.GetActiveScene();
        if (player.activeInHierarchy == true) {
            if (currentScene.name == "Level1") {
                currentLevel = 1;
            } else if (currentScene.name == "Level2Boss") {
                currentLevel = 2;
            }
        } else {
            currentLevel = 0;
        }
        PlayerPrefs.SetInt("Level", currentLevel);

        //Go to Main Menu
        if (Input.GetKeyDown(KeyCode.Q)) {
            SceneManager.LoadScene("Menu");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (currentLevel == 1) {
            SceneManager.LoadScene("Level2Boss");
        }else if(currentLevel == 2) {
            SceneManager.LoadScene("BossScene");
        }
    }
}
