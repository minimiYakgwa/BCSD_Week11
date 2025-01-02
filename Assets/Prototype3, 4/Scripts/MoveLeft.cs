using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30;
    private float leftBound = -15;


    private PlayerController playerControllerScript;

    private void Start()
    {
        playerControllerScript =
            GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (playerControllerScript.gameOver == false)
            transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
            Destroy(gameObject);
    }

}
