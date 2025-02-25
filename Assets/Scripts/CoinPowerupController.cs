
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinPowerup : BasePowerup
{
    // setup this object's type
    // instantiate variables
    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.Coin;
    }
    // no collision for coin "powerup"
    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     if (col.gameObject.CompareTag("Player") && spawned)
    //     {
    //         // TODO: do something when colliding with Player

    //         // then destroy powerup (optional)
    //         DestroyPowerup();

    //     }
    // }

    // interface implementation
    public override void SpawnPowerup()
    {
        Debug.Log("Coin Spawned");
        spawned = true;
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);
        // // PowerupManager.instance.powerupCollected.Invoke(this);
        ApplyPowerup(this);

    }


    public override void ApplyPowerup(MonoBehaviour i)
    {
        GameManager.instance.IncreaseScore(1);

        gameObject.SetActive(false);
    }
}