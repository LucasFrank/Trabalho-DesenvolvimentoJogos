using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public enum PlayerState {
    Standing,
    Running,
    Jumping,
    Falling,
    Attacking,
    Combo,
    StandDown,
    StandUp
};

public class PlayerController : MonoBehaviour {

    public AudioClip jumpSound;
    public AudioClip attackSound;
    public AudioClip comboSound;
    public AudioClip damagedSound;

    private Animator animatorPlayer;
    private Collider2D attackingCollider;
    private Collider2D comboCollider;
    private Rigidbody2D rb;
    private AudioSource asPlayer;

    private PlayerState state;
    private bool grounded = false;
    private float movingSpeed = 5;
    private float jumpForce = 13;
    public static int lives;
    private int maxLives = 3;

    private Vector3 respawnPoint = new Vector3(0, 1, 0);

    public LayerMask groundLayer;

    private float imuneTime = 2.0f;
    private float dt = 0;
    private bool isImune = false;

    private float attackCD = 0.8f;
    private float dt2 = 0;
    private float currentTime = 0;

    public bool pause = false;

    // Use this for initialization
    void Start () {
        this.rb = GetComponent<Rigidbody2D>();
        this.state = PlayerState.Standing;
        this.animatorPlayer = GetComponent<Animator>();
        this.attackingCollider = GameObject.FindGameObjectWithTag("AttackingCollider").GetComponent<Collider2D>();
        this.comboCollider = GameObject.FindGameObjectWithTag("ComboCollider").GetComponent<Collider2D>();
        this.asPlayer = GetComponent<AudioSource>();
        lives = PlayerPrefs.GetInt("lives", 0);
        if(lives == 0){
            lives = 3;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!pause) {
            inputHandler();
            currentTime = Time.time;
            if (currentTime - dt > imuneTime) {
                isImune = false;
            }
        } else {
            this.rb.velocity = Vector3.zero;
        }


        if (Input.GetKeyDown(KeyCode.Escape)) {
            pause = !pause;
            animatorPlayer.enabled = !pause;
        }
    }

    void inputHandler() {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        float currentTime = Time.time;

        if (this.state != PlayerState.StandDown) {
            this.rb.velocity = new Vector3(movingSpeed * dx, this.rb.velocity.y, 0);
            if (dx != 0) {

                if (dx > 0)
                    transform.localScale = new Vector3(-1, 1, 1); //Flipping
                else
                    transform.localScale = new Vector3(1, 1, 1); //Flipping

                if (this.state != PlayerState.Jumping)
                    this.state = PlayerState.Running;
            } else {
                if (this.state != PlayerState.Jumping) {
                    this.state = PlayerState.Standing;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                if (this.grounded) {
                    this.asPlayer.PlayOneShot(jumpSound);
                    this.rb.velocity = new Vector3(this.rb.velocity.x, jumpForce, 0);
                    this.grounded = false;
                    this.state = PlayerState.Jumping;
                }
            }

            if (this.grounded && this.state != PlayerState.Running) {
                if (Input.GetKeyDown(KeyCode.X)) {
                    if (currentTime - dt2 > attackCD) {
                        this.asPlayer.PlayOneShot(attackSound);
                        this.state = PlayerState.Attacking;
                        dt2 = Time.time;
                    }
                }

                if (Input.GetKeyDown(KeyCode.C)) {
                    if (currentTime - dt2 > attackCD) {
                        this.asPlayer.PlayOneShot(comboSound);
                        this.state = PlayerState.Combo;
                        dt2 = Time.time;
                    }
                }
            }
        }

        if(!IsGrounded()) {
            this.state = PlayerState.Falling;
            this.grounded = false;
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
        this.animatorPlayer.SetBool("Falling", this.state == PlayerState.Falling);
        this.animatorPlayer.SetBool("StandUp", this.state == PlayerState.StandUp);
        this.animatorPlayer.SetBool("StandDown", this.state == PlayerState.StandDown);
        this.animatorPlayer.SetBool("Attacking", this.state == PlayerState.Attacking);
        this.animatorPlayer.SetBool("Combo", this.state == PlayerState.Combo);


    }


    private void OnCollisionEnter2D(Collision2D collision) {
        this.grounded = true;
        this.state = PlayerState.Standing;
        
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss") {
            if (!isImune) {
                controlLives(-1);
                dt = Time.time;
                isImune = true;
            }
        }
    }

    private void controlLives(int update) {
       
        int new_life = lives + update;
        if(update < 0)
            asPlayer.PlayOneShot(damagedSound);
        if (new_life <= 0) {
            lives = 0;
            PlayerPrefs.SetInt("lives", lives);
            PlayerPrefs.Save();
            SceneManager.LoadScene("GameOver");
            gameObject.SetActive(false);
        } else if (new_life <= maxLives) {
            lives = new_life;
            PlayerPrefs.SetInt("lives", lives);
            PlayerPrefs.Save();
        }

        
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

    bool IsGrounded() {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(position.x, position.y - 0.5f), direction, distance, groundLayer);
        RaycastHit2D hit1 = Physics2D.Raycast(new Vector2(position.x - 0.5f, position.y - 0.5f), direction, distance, groundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(position.x + 0.5f, position.y - 0.5f), direction, distance, groundLayer);
        Debug.DrawRay(new Vector2(position.x - 0.5f, position.y - 0.5f), direction, Color.green, groundLayer);
        Debug.DrawRay(new Vector2(position.x + 0.2f, position.y - 0.5f), direction, Color.green, groundLayer);
        if (hit.collider != null || hit1.collider != null || hit2.collider != null) {
            return true;
        }

        return false;
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Enemy") {
            Destroy(collision.gameObject);
        }

        if(collision.tag == "Hole") {
            controlLives(-1);
            if (lives > 0) {
                transform.position = respawnPoint;
                isImune = true;
                dt = Time.time;
            }
            
        }

        if(collision.tag == "Checkpoint") {
            Vector3 pos = collision.transform.position;
            respawnPoint = pos;
        }

        if(collision.tag == "Life" && !attackingCollider.enabled && !comboCollider.enabled) {
            controlLives(1);
            Destroy(collision.gameObject);
        }

    }

}
