using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private float originalX;

    private SpriteRenderer goombaSprite;


    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;

    private Vector2 velocity;

    private Rigidbody2D enemyBody;

    public Vector3 startPosition = new(-5.879927f, -3.325205f, 0.0f);


    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        goombaSprite = GetComponent<SpriteRenderer>();

        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
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
}