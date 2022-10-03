using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Collectible : MonoBehaviour
{
    public bool IsCollected { get; protected set; }

    public UnityAction Collected;

    private void Awake()
    {
        IsCollected = false;
    }

    public abstract void Collect();
}
