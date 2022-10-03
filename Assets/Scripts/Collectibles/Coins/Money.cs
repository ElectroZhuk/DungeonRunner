using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : Collectible
{
    [SerializeField] private int _amount;

    public int Amount => _amount;

    public override void Collect()
    {
        if (IsCollected == true)
            throw new UnityException("The element is already collected!");

        IsCollected = true;
        Collected?.Invoke();
        Destroy(gameObject);
    }
}
