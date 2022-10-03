using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected Sprite Image;
    [SerializeField][Min(0)] protected int Price;

    public int Cost => Price;
    public Sprite Icon => Image;

    public abstract void Buy();

    public abstract bool IsBought();
}
