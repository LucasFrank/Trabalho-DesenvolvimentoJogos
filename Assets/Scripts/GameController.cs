using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject playerPrefab;
    private GameObject player;

    private Vector3 spawnPosition;



	// Use this for initialization
	void Start () {
        this.spawnPosition = new Vector3(0, 1, 0);
        this.player = Instantiate<GameObject>(playerPrefab,spawnPosition, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        SceneManager.LoadScene("Level2Boss");
    }
}
