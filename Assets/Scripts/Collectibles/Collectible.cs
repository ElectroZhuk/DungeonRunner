using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Collectible : MonoBehaviour
{
    public bool IsCollected { get; protected set; }

    public event UnityAction Collected;

    private void Awake()
    {
        IsCollected = false;
    }

    public virtual void Collect()
    {
        Collected?.Invoke();
    }
}
