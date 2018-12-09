using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour {

    private int maxHits = 3;
    private int currentHits = 0;

    private int dir = 1;
    private float movingSpeed = 2;

    private bool playerInSight = false;

    private Animator bossAnimator;
    private Rigidbody2D rb;
    private BoxCollider2D ws;

    private float imuneTime = 1.0f;
    private float dt = 0;

    public bool pause = false;

	// Use this for initialization
	void Start () {
        bossAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ws = GetComponentInChildren<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!pause) {
            if (playerInSight) {
                bossAnimator.SetBool("Running", true);
                this.rb.velocity = new Vector3(movingSpeed * dir, this.rb.velocity.y, 0);
                if (transform.position.x > 3)
                    transform.position = new Vector3(3, 2, 0);

                if (transform.position.x < -16.5f)
                    transform.position = new Vector3(-16.5f, 2, 0);
            } else {
                bossAnimator.SetBool("Running", false);
                this.rb.velocity = Vector3.zero;
            }
        } else {
            this.rb.velocity = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pause = !pause;
            bossAnimator.enabled = !pause;
        }


        if (currentHits == maxHits) {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            playerInSight = true;
        }
        float currentTime = Time.time;
        if (currentTime - dt > imuneTime) { //Delaying attacks
            if (collision.tag == "AttackingCollider" && ws.tag == "WeakSpot") {
                currentHits += 1;
                dt = Time.time;
                dir *= -1;
                transform.localScale = new Vector3(-1.5f * dir, 1.5f, 0); // Turning boss
            }

            if (collision.tag == "ComboCollider" && ws.tag == "WeakSpot") {
                currentHits += 1;
                dt = Time.time;
                dir *= -1;
                transform.localScale = new Vector3(-1.5f * dir, 1.5f, 0); // Turning boss
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            playerInSight = false;
        }
    }
}
