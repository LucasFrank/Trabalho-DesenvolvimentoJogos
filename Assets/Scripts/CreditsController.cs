using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour {

    public VideoPlayer player;
    private float dt = 0;
    private float currentTime;
    public Text canvasText;

    private int maxPosition = 460;
    private float speed = 50.0f;

    private bool playOnce = false;

    // Use this for initialization
    void Start () {
        canvasText.rectTransform.localPosition = new Vector3(0, -460, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (canvasText.rectTransform.localPosition.y < maxPosition) {
            canvasText.rectTransform.localPosition += new Vector3(0, speed * Time.deltaTime, 0);
        } else {
            dt = Time.time;
            if (!playOnce) {
                this.player.Play();
                playOnce = true;
                this.currentTime = Time.time;
            }else if (dt - this.currentTime >= 2.0 && !this.player.isPlaying) {   
                SceneManager.LoadScene("Menu");

            }
        }
	}
}
