using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.PackageManager;

public class JumpOverGoomba : MonoBehaviour
{
    public Transform enemyLocation;

    // public TextMeshProUGUI scoreText;
    private bool onGroundState;

    [System.NonSerialized]
    // public int score = 0; // we don't want this to show up in the inspector

    private bool countScoreState = false;
    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // mario jumps
        if (Input.GetKeyDown("space") && onGroundCheck())
        {
            onGroundState = false;
            countScoreState = true;
        }

        // when jumping, and Goomba is near Mario and we haven't registered our score
        if (!onGroundState && countScoreState)
        {
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                // SharedData.Instance.IncrementScore(1);
                // disabling jump over goomba
                // gameManager.IncreaseScore(1);


            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    }


    private bool onGroundCheck()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask))
        {
            // Debug.Log("on ground");
            return true;
        }
        else
        {
            // Debug.Log("not on ground");
            return false;
        }
    }
}
