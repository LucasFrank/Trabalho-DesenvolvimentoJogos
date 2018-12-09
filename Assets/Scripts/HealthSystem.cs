using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour {

	public Sprite[] Hearts;
	public Image Heart;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Heart.sprite = Hearts[PlayerController.lives];
	}
}
