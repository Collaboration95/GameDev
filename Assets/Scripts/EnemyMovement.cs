using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{

    private float originalX;

    private SpriteRenderer goombaSprite;


    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;

    private Vector2 velocity;

    private Rigidbody2D enemyBody;

    public Vector3 startPosition = new(2.334109f, -2.911f, 0.11265f);

    public Animator goombaAnimator;


    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        goombaSprite = GetComponent<SpriteRenderer>();

        // get the starting position
        originalX = startPosition.x;
        ComputeVelocity();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Update start position for the new scene
        startPosition = transform.position;
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }

    public void GameRestart()
    {
        transform.position = startPosition;
        // originalX is already set in Start() and shouldn't change
        moveRight = -1;
        ComputeVelocity();

        this.enabled = true;  // Re-enable movement script
        GetComponent<SpriteRenderer>().enabled = true;  // Show 
        Debug.Log("EnemyMovement GameRestart");
        goombaAnimator.SetTrigger("gameRestart");
        GetComponent<Collider2D>().enabled = true;  // Enable collision
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(other.gameObject.name);
    }
    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {// move goomba
            Movegoomba();
        }
        else
        {
            // change direction
            goombaSprite.flipX = moveRight > 0;
            moveRight *= -1;
            ComputeVelocity();
            Movegoomba();
        }
    }

    public void Die()
    {
        Debug.Log("A peaceful Goomba was attacked by Mario");
        this.enabled = false;
        if (TryGetComponent<Animator>(out Animator animator))
        {
            animator.Play("goomba-dead");
        }
        StartCoroutine(DeathEffect());
    }


    IEnumerator DeathEffect()
    {
        yield return new WaitForSeconds(0.3f); // Wait for animation
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

    }
}