using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator animationPlayer;
    private float walkingSpeed;

	// Use this for initialization
	void Start () {
        walkingSpeed = 0.02f;
        animationPlayer = GetComponent<Animator>();
        animationPlayer.SetBool("Walking", false);
    }
	
	// Update is called once per frame
	void Update () {
        inputHandler();
	}

    void inputHandler() {
        if (Input.GetKey(KeyCode.RightArrow)) {
            if (animationPlayer.GetBool("Walking") == false) {
                animationPlayer.SetBool("Walking", true);
            }
            
            transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(new Vector3(walkingSpeed, 0, 0));
            //transform.position += new Vector3(walkingSpeed, 0, 0);

        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            if (animationPlayer.GetBool("Walking") == false) {
                animationPlayer.SetBool("Walking", true);
            }
            transform.localScale = new Vector3(-1, 1, 1);
            transform.Translate(new Vector3(-walkingSpeed, 0, 0));
            //transform.position += new Vector3(walkingSpeed, 0, 0);

        } else {
            animationPlayer.SetBool("Walking", false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            //rd.velocity = new Vector3(0, jumpForce, 0);
            //rd.AddForce(new Vector3(0, jumpForce, 0));
            //animation.SetBool("Jumping", true);


        }
    }
}
