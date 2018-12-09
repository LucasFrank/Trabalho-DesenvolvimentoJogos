using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty { // movimentação por tempo, posição ou aleatorio
    Time,
    Position,
    Random
};

public class EnemyController : MonoBehaviour {

    private Animator animatorEnemy;
    private Rigidbody2D rb;
    private bool alive = true;
    private int dir = 1;
    private float movingSpeed = 2;
    private float auxTime = 0;

    private Difficulty type;

    // Use this for initialization
    void Start () {
        this.type = Difficulty.Time;
        this.animatorEnemy = GetComponent<Animator>();
        this.rb = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        if (alive) {
            if (this.type == Difficulty.Time) {
                float currentTime = Time.time;
                this.rb.velocity = new Vector3(-movingSpeed * dir, this.rb.velocity.y, 0);
                transform.localScale = new Vector3(dir, 1, 1);

                if (currentTime - auxTime > 2) {
                    dir = dir * -1;
                    auxTime = currentTime;
                }
            }

        }
	}

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "AttackingCollider") {
            Destroy(gameObject);
        }

        if (coll.gameObject.tag == "ComboCollider") {
            Destroy(gameObject);
        }
    }

}
