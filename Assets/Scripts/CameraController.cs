using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject player;

    private float leftBound;
    private float rightBound;

    private float upperBound;
    private float lowerBound;

    private float smoothDampTime = 0.15f;
    private Vector3 smoothDampVelocity = Vector3.zero;

    private float camWidth, camHeight, levelMinX, levelMaxX, levelMinY, levelMaxY;

    private Scene scene;

    // Use this for initialization
    void Start() {
        scene = SceneManager.GetActiveScene();

        player = GameObject.FindGameObjectWithTag("Player");

        camHeight = Camera.main.orthographicSize * 2;
        camWidth = camHeight * Camera.main.aspect;

        leftBound = GameObject.FindGameObjectWithTag("LeftBound").transform.position.x;
        rightBound = GameObject.FindGameObjectWithTag("RightBound").transform.position.x;

        if (scene.name == "Level1") {
            upperBound = GameObject.FindGameObjectWithTag("Roof").transform.position.y;
            lowerBound = GameObject.FindGameObjectWithTag("Ground").transform.position.y;

            levelMinY = lowerBound + (camHeight / 2);
            levelMaxY = upperBound - (camHeight / 2);

            levelMinX = leftBound + (camWidth / 2);
            levelMaxX = rightBound - (camWidth / 2); 
        } else {

            levelMinX = leftBound + (camWidth / 2);
            levelMaxX = rightBound - (camWidth / 2);
        }
    }

    // Update is called once per frame
    void Update() {
        if (player) {

            if (scene.name == "Level1") {

                float targetX = Mathf.Max(levelMinX, Mathf.Min(levelMaxX, player.transform.position.x));
                float targetY = Mathf.Max(levelMinY, Mathf.Min(levelMaxY, player.transform.position.y));

                float x = Mathf.SmoothDamp(transform.position.x, targetX, ref smoothDampVelocity.x, smoothDampTime);
                float y = Mathf.SmoothDamp(transform.position.y, targetY, ref smoothDampVelocity.y, smoothDampTime);

                transform.position = new Vector3(x, y, transform.position.z);
            } else {
                Debug.Log("test");
                float targetX = Mathf.Max(levelMinX, Mathf.Min(levelMaxX, player.transform.position.x));

                float x = Mathf.SmoothDamp(transform.position.x, targetX, ref smoothDampVelocity.x, smoothDampTime);

                transform.position = new Vector3(x, transform.position.y, transform.position.z);
            }
        }
    }
}
