using UnityEngine;

public enum PowerupType
{
    Undefined,
    Speed,
    Strength,
    Shield,
    MagicMushroom,
    Coin,
}

public interface IPowerup
{
    void DestroyPowerup();
    void SpawnPowerup();
    void ApplyPowerup(MonoBehaviour i);

    PowerupType powerupType
    {
        get;
    }

    bool hasSpawned
    {
        get;
    }
}


public interface IPowerupApplicable
{
    public void RequestPowerupEffect(IPowerup i);
}