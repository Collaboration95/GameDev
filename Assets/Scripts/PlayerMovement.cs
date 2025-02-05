using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
public class PlayerMovement : MonoBehaviour

{
    // global variables
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    public float speed = 10;
    private Rigidbody2D marioBody;

    public TextMeshProUGUI scoreText;
    public GameObject enemies;

    // Start is called before the first frame update
    void Start()
    {

        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();

        // To flip sprite based on Mario's movement
        marioSprite = GetComponent<SpriteRenderer>();




    }

    public void RestartButtonCallback(int input)
    {
        // if (input == 1)
        // {
        //     Debug.Log("Restart called GameOver");
        // }
        // else if (input == 0)
        // {
        //     Debug.Log("Restart called from game");
        // }

        // reset everything
        ResetGame();

        // resume time
        Time.timeScale = 1.0f;
    }

    private void ResetGame()
    {
        // reset position
        marioBody.transform.position = new Vector3(-5.879927f, -3.325205f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;
        // reset score
        scoreText.text = "Score: 0"; // getting an error here ( during restart , not able to fix this and set timescale to 1.0f)
                                     // reset 

        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
            // Debug.Log("Start positon of enemy is" + eachChild.transform.localPosition);
        }

        //Resetting scores
        SharedData.Instance.ResetScore();

        // remove GameOverScreen
        GameOverScript.HideGameOverScreen();


    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
        }
        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
        }
    }

    // Fixed update may happen less than once per frame at high framerates and multiple times at lower frame rates
    public float maxSpeed = 20;
    public float upSpeed = 10;
    private bool onGroundState = true;

    public GameOverScript GameOverScript;



    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Debug.Log("Collided with goomba! , Need to restart game here!");

            Time.timeScale = 0.0f;

            GameOverScript.Setup(SharedData.Instance.score);
        }
    }

    // FixedUpdate() is for things impacting physics engine stuff 

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new(moveHorizontal, 0);
            // check if it doesn't go beyond maxSpeed
            if (marioBody.velocity.magnitude < maxSpeed)
                marioBody.AddForce(movement * speed);
        }

        // stop
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            // stop
            marioBody.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
        }
    }
}