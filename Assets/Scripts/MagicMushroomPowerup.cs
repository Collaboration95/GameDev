
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMushroomPowerup : BasePowerup
{
    // setup this object's type
    // instantiate variables
    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.MagicMushroom;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && spawned)
        {
            // TODO: do something when colliding with Player

            // then destroy powerup (optional)
            DestroyPowerup();

        }
        else if (col.gameObject.layer == 6) // else if hitting Pipe, flip travel direction
        {
            if (spawned)
            {
                Debug.Log("Collided with Pipe");
                goRight = !goRight;
                // rigidBody.AddForce(Vector2.right * 3 * (goRight ? 1 : -1), ForceMode2D.Impulse);
                rigidBody.AddForce(Vector2.left * 5, ForceMode2D.Impulse); // move to the right

            }
        }
        else
        {
            Debug.Log("Collided with something else" + col.gameObject.layer);
        }
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        Debug.Log("Something happened ?");
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        this.GetComponent<BoxCollider2D>().enabled = true;
        // Collider2D collider = GetComponent<Collider2D>();


        spawned = true;
        rigidBody.AddForce((Vector2.up * 2 + Vector2.right * 5), ForceMode2D.Impulse); // move upward and to the right
    }


    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        // TODO: do something with the object

    }
}