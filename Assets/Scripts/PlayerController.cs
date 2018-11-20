using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    Standing,
    Running,
    Jumping,
    Attacking,
    Combo,
    StandDown,
    StandUp
};

public class PlayerController : MonoBehaviour {

    private Animator animatorPlayer;
    private Collider2D attackingCollider;
    private Collider2D comboCollider;

    private PlayerState state;
    private bool grounded = false;
    private float movingSpeed = 5;
    private float jumpForce = 5;

    // Use this for initialization
    void Start () {
        this.state = PlayerState.Standing;
        this.animatorPlayer = GetComponent<Animator>();
        this.attackingCollider = GameObject.FindGameObjectWithTag("AttackingCollider").GetComponent<Collider2D>();
        this.comboCollider = GameObject.FindGameObjectWithTag("ComboCollider").GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        inputHandler();
	}

    void inputHandler() {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        GetComponent<Rigidbody2D>().velocity = new Vector3(movingSpeed * dx, GetComponent<Rigidbody2D>().velocity.y, 0);

        if (this.state != PlayerState.StandDown) {
            if (dx != 0) {

                if (dx > 0)
                    transform.localScale = new Vector3(-1, 1, 1); //Flipping
                else
                    transform.localScale = new Vector3(1, 1, 1); //Flipping

                if (this.state != PlayerState.Jumping)
                    this.state = PlayerState.Running;
            } else {
                if (this.state != PlayerState.Jumping)
                    this.state = PlayerState.Standing;
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                if (this.grounded) {
                    GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, jumpForce, 0);
                    this.grounded = false;
                    this.state = PlayerState.Jumping;
                }
            }

            if (Input.GetKeyDown(KeyCode.X)) {
                this.state = PlayerState.Attacking;
            }

            if (Input.GetKeyDown(KeyCode.C)) {
                this.state = PlayerState.Combo;
            }
        }

        if(dy != 0) {
            if (dy > 0) {
                if(this.state == PlayerState.StandDown)
                    this.state = PlayerState.StandUp;
            } else {
                if(this.state == PlayerState.Standing)
                    this.state = PlayerState.StandDown;
            }
        }

        this.animatorPlayer.SetBool("Running", this.state == PlayerState.Running);
        this.animatorPlayer.SetBool("Jumping", this.state == PlayerState.Jumping);
        this.animatorPlayer.SetBool("StandUp", this.state == PlayerState.StandUp);
        this.animatorPlayer.SetBool("StandDown", this.state == PlayerState.StandDown);
        this.animatorPlayer.SetBool("Attacking", this.state == PlayerState.Attacking);
        this.animatorPlayer.SetBool("Combo", this.state == PlayerState.Combo);
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        this.grounded = true;
        this.state = PlayerState.Standing;
    }

    public void activateAttacking() {
        this.attackingCollider.enabled = true;
        this.attackingCollider.isTrigger = true;
    }

    public void activateCombo() {
        this.comboCollider.enabled = true;
        this.comboCollider.isTrigger = true;
    }

    public void deactivateAttacking() {
        this.attackingCollider.enabled = false;
        this.attackingCollider.isTrigger = false;
    }

    public void deactivateCombo() {
        this.comboCollider.enabled = false;
        this.comboCollider.isTrigger = false;
    }


}
