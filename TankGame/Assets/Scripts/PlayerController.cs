using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public GameObject projectilePrefab;
    public GameObject powerUpIndicator;
    public ParticleSystem explosionParticle;
    public ParticleSystem fireParticle;
    public GameManager gameManager;
    public float speed = 10.0f;
    public float turnSpeed = 50.0f;
    private float horizontalInput;
    private float forwardInput;
    private float xRange = 20;
    private float zRange = 10;
    public bool playerIsAlive;
    public bool hasPower;

    // Start is called before the first frame update
    void Start()
    {
        playerIsAlive = true;
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
       if (playerIsAlive == true /*&& gameManager.gameIsActive*/) {
            MovePlayer();
            ConstrainPlayerPosition(); 
        }
    }

    
    
    //Moves and turns the player based on arrow key input and fires projectile when Space is pressed
    void MovePlayer()
    {
        //Player moves forward/backwards when the up/down arrow keys are pressed
        //Player turns when the left/right arrow keys are pressed
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        if (hasPower == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed * 2 * forwardInput);
            transform.Rotate(Vector3.up, turnSpeed * 3 * horizontalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
        }



        //Player fires projectile when space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position + (transform.forward * 2), transform.rotation);
        }
    
    }

    //Prevents player from leaving the screen
    void ConstrainPlayerPosition()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }

        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            hasPower = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerCountdown());
            powerUpIndicator.gameObject.SetActive(true);
        }
    }

    IEnumerator PowerCountdown()
    {
        yield return new WaitForSeconds(5);
        hasPower = false;
        powerUpIndicator.gameObject.SetActive(false);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerIsAlive = false;
            Debug.Log("Game Over");
            explosionParticle.Play();
            fireParticle.Play();
        } 
    }
}
