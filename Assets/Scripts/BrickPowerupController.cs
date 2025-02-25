using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPowerupController : MonoBehaviour, IPowerupController
{
    public Animator powerupAnimator;
    public BasePowerup powerup; // reference to this question box's powerup

    public bool isBreakable = false;


    private SpriteRenderer spriteRenderer;
    public Animator BlockAnimator;

    public AudioSource audioSource;
    void Start()
    {
        // hasCoin = Random.value < 0.5f;
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // (Assuming that when the player hits from below, the contact normal's Y is greater than 0.5.)
        if (collision.contacts[0].normal.y > 0.5f)
        {
            if (!powerup.spawned)
            {
                BlockAnimator.SetBool("Blinking", false);

                powerupAnimator.SetTrigger("spawned");
            }
            if (isBreakable)
            {
                this.Disable();
            }

        }
    }
    public void Disable()
    {
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void gameRestart()
    {
        Debug.Log("BrickPowerupController Restart : Empty Function For Now ");
    }
}