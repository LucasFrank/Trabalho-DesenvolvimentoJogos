using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    private int maxHits = 6;
    private int currentHits = 0;

    private int dir = 1;
    private float movingSpeed = 2;

    private bool playerInSight = false;

    private Animator bossAnimator;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        bossAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (playerInSight) {
            bossAnimator.SetBool("Running", true);
            this.rb.velocity = new Vector3(movingSpeed * dir, this.rb.velocity.y, 0);
        } else {
            bossAnimator.SetBool("Running", false);
            this.rb.velocity = Vector3.zero;
        }

        if(currentHits == maxHits) {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            playerInSight = true;
        }

        if (collision.tag == "AttackingCollider") {
            currentHits += 1;
        }

        if (collision.tag == "ComboCollider") {
            currentHits += 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            playerInSight = false;
        }
    }
}
