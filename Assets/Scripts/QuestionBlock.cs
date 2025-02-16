

using UnityEngine;
using System.Collections;

public class QuestionBlockHit : MonoBehaviour
{

    private bool hasBeenHit = false;

    [Header("Block Settings")]
    [SerializeField] private Sprite usedSprite;
    public Animator BlockAnimator;

    [Header("Coin Settings")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float coinMoveDistance = 1.0f;
    [SerializeField] private float coinMoveSpeed = 4.0f;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private Vector3 coinSpawnOffset = new Vector3(0, 0.0f, 0);

    private SpriteRenderer spriteRenderer;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // (Assuming that when the player hits from below, the contact normal's Y is greater than 0.5.)
        if (collision.contacts[0].normal.y > 0.5f)
        {
            // Process only if this is the first valid hit.
            if (!hasBeenHit)
            {
                // Debug.Log("Block hit for the first time!");


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
            }
        }
    }

    private IEnumerator SpawnAndAnimateCoin()
    {
        Vector3 spawnPosition = transform.position + coinSpawnOffset;

        GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);

        audioSource.PlayOneShot(audioSource.clip);

        Vector3 peakPosition = spawnPosition + Vector3.up * coinMoveDistance;

        while (Vector3.Distance(coin.transform.position, peakPosition) > 0.01f)
        {
            coin.transform.position = Vector3.MoveTowards(coin.transform.position, peakPosition, coinMoveSpeed * Time.deltaTime);
            yield return null;
        }

        while (Vector3.Distance(coin.transform.position, spawnPosition) > 0.01f)
        {
            coin.transform.position = Vector3.MoveTowards(coin.transform.position, spawnPosition, coinMoveSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        Destroy(coin);
    }

    private IEnumerator ReturnToOriginalAndSetStatic()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            yield break;
        }

        while (rb.velocity.sqrMagnitude > 0.001f)
        {
            yield return null;
        }

        rb.bodyType = RigidbodyType2D.Static;

        SpringJoint2D springJoint = GetComponent<SpringJoint2D>();
        if (springJoint != null)
        {
            springJoint.enabled = false;
        }
    }
}
