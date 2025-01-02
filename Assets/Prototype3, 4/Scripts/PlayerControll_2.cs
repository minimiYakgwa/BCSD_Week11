using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControll_2 : MonoBehaviour
{
    private float speed = 1.0f;
    private float jumpPower = 10.0f;
    private float horizontalInput;
    private float verticalInput;
    private bool isJumping = false;
    private bool isTrapped = false;
    private Vector3 direction;
    private Renderer playerMt;

    private Rigidbody playerRb;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerMt = GetComponent<Renderer>();
    }

    private void Update()
    {
        Jump();
        Reposition();

        
    }

    private void FixedUpdate()
    {
        Move();   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            StartCoroutine(Trapped());

        }
    }

    private IEnumerator Trapped()
    {
        Debug.Log("Trapped!!!");
        isTrapped = true;
        playerRb.velocity = Vector3.zero;
        playerRb.isKinematic = true;
        yield return new WaitForSeconds(5f);

        gameObject.layer = 6;
        isTrapped = false;
        playerRb.isKinematic = false;
        int timer = (int)Time.deltaTime;

        while (timer < 5)
        {
            Color color = playerMt.material.color;
            color.a = (color.a == 1f) ? 0f : 1f;
            playerMt.material.color = color;
            yield return new WaitForSeconds(1f);
            timer += 1;
        }
            

        gameObject.layer = 7;
        

    }

    private void Move()
    {
        if (isJumping)
        {
            return;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontalInput, 0, verticalInput);
        
        playerRb.AddForce(direction * speed, ForceMode.Impulse);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            playerRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void Reposition()
    {
        if (Mathf.Abs(transform.position.x) > 30 || Mathf.Abs(transform.position.z) > 30)
        {
            playerRb.velocity = Vector3.zero;
            transform.position = new Vector3(0, 2, 0);
        }
    }
}
