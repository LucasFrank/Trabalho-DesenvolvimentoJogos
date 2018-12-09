using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CinematicController : MonoBehaviour {

    private float dt = 0;
    private float currentTime;
    public Canvas canvasText;
    public VideoPlayer player;

    

    // Use this for initialization
    void Start () {
        this.currentTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        this.dt = Time.time;
		if(dt - this.currentTime >= 5.0 && dt - this.currentTime < 7.0) {
            canvasText.GetComponentInChildren<Text>().enabled = false;
            this.player.Play();
        }
        if ((dt - this.currentTime >= 7.0 && !this.player.isPlaying) || Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Level1");

	}


}
