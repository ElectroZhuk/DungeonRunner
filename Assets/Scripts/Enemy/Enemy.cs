using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] [Min(1)] int _level;

    public int Level => _level;

    public UnityAction Attacking;
    public UnityAction Dead;
    public UnityAction<int> LevelChanged;

    private void Start()
    {
        LevelChanged?.Invoke(_level);
    }

    public void Attack()
    {
        Attacking?.Invoke();
    }

    public void Kill()
    {
        Dead.Invoke();
    }
}
