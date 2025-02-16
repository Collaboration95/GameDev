

using UnityEngine;
using System.Collections;

public class QuestionBlockHit : MonoBehaviour
{
    // Flag to ensure we only change the sprite/spawn coin on the first hit.
    private bool hasBeenHit = false;

    [Header("Block Settings")]
    [SerializeField] private Sprite usedSprite;       // The sprite after being hit.
    public Animator BlockAnimator;                      // Animator for the block (if any).

    [Header("Coin Settings")]
    [SerializeField] private GameObject coinPrefab;     // Coin prefab to spawn.
    [SerializeField] private float coinMoveDistance = 1.0f; // How high the coin moves upward.
    [SerializeField] private float coinMoveSpeed = 4.0f;    // Speed of coin movement.
    [SerializeField] private AudioClip coinSound;         // Sound effect when coin spawns.
    [SerializeField] private Vector3 coinSpawnOffset = new Vector3(0, 0.0f, 0); // Offset relative to block position.

    // Cached reference to the SpriteRenderer.
    private SpriteRenderer spriteRenderer;

    // Cached original position of the block (for other use if needed).
    private Vector3 originalPosition;

    // Audio source to play the coin sound.
    public AudioSource audioSource;

    private void Awake()
    {
        // Cache the SpriteRenderer.
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Missing SpriteRenderer on the Question Block!");
        }

        // Store the block's original position.
        originalPosition = transform.position;

        // Get or add an AudioSource component.
        // audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision contact indicates a hit from below.
        // (Assuming that when the player hits from below, the contact normal's Y is greater than 0.5.)
        if (collision.contacts[0].normal.y > 0.5f)
        {
            // Process only if this is the first valid hit.
            if (!hasBeenHit)
            {
                Debug.Log("Block hit for the first time!");

                // Change the sprite to the used block sprite.
                if (usedSprite != null)
                {
                    if (BlockAnimator != null)
                    {
                        BlockAnimator.enabled = false;
                    }
                    spriteRenderer.sprite = usedSprite;
                }
                else
                {
                    Debug.LogWarning("Used sprite is not assigned!");
                }

                hasBeenHit = true;


                StartCoroutine(ReturnToOriginalAndSetStatic());
                StartCoroutine(SpawnAndAnimateCoin());


                // If you want the block to eventually become static, you can start another coroutine
                // (for example, ReturnToOriginalAndSetStatic) after the coin animation.
            }
        }
    }

    private IEnumerator SpawnAndAnimateCoin()
    {
        // Calculate the spawn position for the coin.
        // Set the coinSpawnOffset so that the coin spawns inside the block.
        Vector3 spawnPosition = transform.position + coinSpawnOffset;

        // Instantiate the coin prefab.
        GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);

        audioSource.PlayOneShot(audioSource.clip);
        // Animate the coin: move upward until it reaches a peak, then move back down.
        Vector3 peakPosition = spawnPosition + Vector3.up * coinMoveDistance;

        // Animate upward.
        while (Vector3.Distance(coin.transform.position, peakPosition) > 0.01f)
        {
            coin.transform.position = Vector3.MoveTowards(coin.transform.position, peakPosition, coinMoveSpeed * Time.deltaTime);
            yield return null;
        }

        // Animate downward back to the spawn position (inside the box).
        while (Vector3.Distance(coin.transform.position, spawnPosition) > 0.01f)
        {
            coin.transform.position = Vector3.MoveTowards(coin.transform.position, spawnPosition, coinMoveSpeed * Time.deltaTime);
            yield return null;
        }

        // Optionally, wait a brief moment at the spawn position.
        yield return new WaitForSeconds(0.1f);

        // Remove the coin.
        Destroy(coin);
    }

    private IEnumerator ReturnToOriginalAndSetStatic()
    {

        // Cache the Rigidbody2D component.
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            yield break;
        }

        // Wait until the block's velocity is near zero.
        while (rb.velocity.sqrMagnitude > 0.001f)
        {
            yield return null;
        }

        // Set the Rigidbody2D to Static so it no longer moves.
        rb.bodyType = RigidbodyType2D.Static;


        // Also disable the SpringJoint2D if it's still active.
        SpringJoint2D springJoint = GetComponent<SpringJoint2D>();
        if (springJoint != null)
        {
            springJoint.enabled = false;
        }
    }
}
