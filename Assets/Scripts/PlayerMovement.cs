using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
public class PlayerMovement : MonoBehaviour

{
    public AudioSource marioAudio;
    public Transform gameCamera;
    public AudioClip marioDeath;
    public float deathImpulse = 15;
    [System.NonSerialized]
    public bool alive = true;

    // global variables
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    public float speed = 10;
    private Rigidbody2D marioBody;

    public TextMeshProUGUI scoreText;
    public GameObject enemies;

    // Start is called before the first frame update

    public Animator marioAnimator;

    void Start()
    {

        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();

        // To flip sprite based on Mario's movement
        marioSprite = GetComponent<SpriteRenderer>();

        marioAnimator.SetBool("onGround", onGroundState);
    }

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);

    }

    void GameOverScene()
    {
        Time.timeScale = 0.0f;
        GameOverScript.Setup(SharedData.Instance.score);
    }

    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
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


        Debug.Log("GameOverScreenIsCalled");
        // remove GameOverScreen
        GameOverScript.HideGameOverScreen();

        //reset animation
        marioAnimator.SetTrigger("gameRestart");

        alive = true;

        // reset camera position
        gameCamera.position = new Vector3(0, 0, -10);

    }


    // // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown("a") && faceRightState)
    //     {
    //         faceRightState = false;
    //         marioSprite.flipX = true;
    //         if (marioBody.velocity.x > 0.1f)
    //             marioAnimator.SetTrigger("onSkid");
    //     }
    //     if (Input.GetKeyDown("d") && !faceRightState)
    //     {
    //         faceRightState = true;
    //         marioSprite.flipX = false;
    //         if (marioBody.velocity.x < -0.1f)
    //             marioAnimator.SetTrigger("onSkid");

    //     }

    //     marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    // }


    void Update()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    void FlipMarioSprite(int value)
    {
        if (value == -1 && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.05f)
                marioAnimator.SetTrigger("onSkid");

        }

        else if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.05f)
                marioAnimator.SetTrigger("onSkid");
        }
    }


    // Fixed update may happen less than once per frame at high framerates and multiple times at lower frame rates
    public float maxSpeed = 20;
    public float upSpeed = 10;
    private bool onGroundState = true;

    public GameOverScript GameOverScript;
    int collisionLayerMask = (1 << 3) | (1 << 6) | (1 << 7);
    void OnCollisionEnter2D(Collision2D col)
    {

        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & !onGroundState)
        {
            onGroundState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && alive)
        {
            // Debug.Log("Collided with goomba! , Need to restart game here!");

            marioAnimator.Play("mario-die");
            marioAudio.PlayOneShot(marioDeath);
            alive = false;
        }
    }

    // FixedUpdate() is for things impacting physics engine stuff 
    private bool moving = false;
    void FixedUpdate()
    {
        if (alive && moving)
        {
            Move(faceRightState == true ? 1 : -1);
        }
    }

    void Move(int value)
    {

        Vector2 movement = new Vector2(value, 0);
        // check if it doesn't go beyond maxSpeed
        if (marioBody.velocity.magnitude < maxSpeed)
            marioBody.AddForce(movement * speed);
    }

    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }

    private bool jumpedState = false;

    public void Jump()
    {
        if (alive && onGroundState)
        {
            // jump
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);

        }
    }


    public void JumpHold()
    {
        if (alive && jumpedState)
        {
            // jump higher
            marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;

        }
    }





    // void FixedUpdate()
    // {

    //     if (alive)
    //     {

    //         float moveHorizontal = Input.GetAxisRaw("Horizontal");

    //         if (Mathf.Abs(moveHorizontal) > 0)
    //         {
    //             Vector2 movement = new(moveHorizontal, 0);
    //             // check if it doesn't go beyond maxSpeed
    //             if (marioBody.velocity.magnitude < maxSpeed)
    //                 marioBody.AddForce(movement * speed);
    //         }

    //         // stop
    //         if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
    //         {
    //             // stop
    //             marioBody.velocity = Vector2.zero;
    //         }

    //         if (Input.GetKeyDown("space") && onGroundState)
    //         {
    //             marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
    //             onGroundState = false;

    //             marioAnimator.SetBool("onGround", onGroundState);
    //         }

    //     }

    // }
}