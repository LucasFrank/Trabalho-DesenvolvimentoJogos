using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty { // movimentação por tempo, posição ou aleatorio
    Time,
    Follow,
};

public class EnemyController : MonoBehaviour {

    private Animator animatorEnemy;
    public GameObject life;
    private Rigidbody2D rb;
    private bool alive = true;
    private int dir = 1;
    private float movingSpeed = 2;
    private float auxTime = 0;

    private float lifeChance = 4;

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
            }else if (this.type == Difficulty.Follow) {
                this.rb.velocity = new Vector3(-movingSpeed * dir, this.rb.velocity.y, 0);
                //Keep going in the princess direction since it is the colliders position direction
            }

        }
        
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        PolygonCollider2D pc = GetComponent<PolygonCollider2D>();
        if (coll.tag == "AttackingCollider" && coll.IsTouching(pc)) {
            int spawnChance = Random.Range(0, 10);
            if(spawnChance < lifeChance) {
                Instantiate<GameObject>(life, gameObject.transform.position, Quaternion.identity);
            }
        }
        if (coll.tag == "ComboCollider" && coll.IsTouching(pc)) {

            int spawnChance = Random.Range(0, 10);
            if (spawnChance < lifeChance) {
                Instantiate<GameObject>(life, gameObject.transform.position, Quaternion.identity);
            }
        }

        if(coll.tag == "Player") {
            this.type = Difficulty.Follow;
        }
    }

    private void OnTriggerExit2D(Collider2D coll) {
        if (coll.tag == "Player") {
            this.type = Difficulty.Time;
        }
    }

}
