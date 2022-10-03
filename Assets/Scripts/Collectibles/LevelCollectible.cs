using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCollectible : Collectible
{
    [SerializeField] private int _numberOfLevels;

    public int NumberOfLevels => _numberOfLevels;

    public override void Collect()
    {
        if (IsCollected == true)
            throw new UnityException("The element is already collected!");

        IsCollected = true;
        Collected?.Invoke();
        Destroy(gameObject);
    }
}
