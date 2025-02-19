using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    GameManager gameManager;
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
    public Animator marioAnimator;

    public AudioSource marioDeathAudio;


    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();

        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();

        marioSprite = GetComponent<SpriteRenderer>();

        marioAnimator.SetBool("onGround", onGroundState);
    }

    public bool IsGrounded()
    {
        // exposing mario's state to pitchshifter
        return onGroundState;
    }

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    void GameOverScene()
    {
        Time.timeScale = 0.0f;
        gameManager.GameOver();
        Debug.Log("GameOverScene is being called\n");
    }

    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    public void GameRestart()
    {
        // reset position
        marioBody.transform.position = new Vector3(-5.879927f, -3.325205f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;

        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;

        // reset camera position
        gameCamera.position = new Vector3(0, 0, -10);
    }

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
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && alive)
        {
            if (Mathf.Abs(marioBody.velocity.y) > 0)
            {
                Debug.Log("Righteous Mario Vanquishes Goomba");
                gameManager.IncreaseScore(5);

                EnemyMovement temp = other.gameObject.GetComponent<EnemyMovement>();
                if (temp != null)
                {
                    temp.Die();
                }
            }
            else
            {
                marioAnimator.Play("mario-die");
                marioDeathAudio.PlayOneShot(marioDeathAudio.clip);
                alive = false;
            }
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
            marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;
        }
    }
}