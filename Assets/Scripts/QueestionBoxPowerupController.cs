using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxPowerupController : MonoBehaviour, IPowerupController
{
    public Animator powerupAnimator;
    public BasePowerup powerup; // reference to this question box's powerup

    private bool hasBeenHit = false;

    private SpriteRenderer spriteRenderer;
    public Animator BlockAnimator;

    public AudioSource audioSource;

    private void Awake()
    {
        // Cache the SpriteRenderer.
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Missing SpriteRenderer on the Question Block!");
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !hasBeenHit && other.contacts[0].normal.y > 0.5f)
        {
            hasBeenHit = true;

            BlockAnimator.SetBool("Blinking", false);
            powerupAnimator.SetTrigger("spawned");

        }
    }
    public void Disable()
    {
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void gameRestart()
    {
        Debug.Log("QuestionBoxPowerupController Restart : Empty Function For Now ");
    }

}