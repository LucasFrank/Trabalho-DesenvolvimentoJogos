using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "AttackingCollider") {
            Debug.Log("uhhh");
            Destroy(gameObject);
        }

        if (coll.gameObject.tag == "ComboCollider") {
            Debug.Log("aaaaaahhh");
            Destroy(gameObject);
        }
    }

}
