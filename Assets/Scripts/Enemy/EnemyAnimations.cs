using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Animator _animator;

    public event UnityAction EnemyAttacking;
    public event UnityAction EnemyDead;

    private void OnEnable()
    {
        _enemy.Attacking += OnAttacking;
        _enemy.Dead += OnDead;
    }

    private void OnDisable()
    {
        _enemy.Attacking -= OnAttacking;
        _enemy.Dead -= OnDead;
    }

    public void OnEnemyAttacking()
    {
        EnemyAttacking?.Invoke();
    }

    public void OnEnemyDead()
    {
        EnemyDead?.Invoke();
    }

    private void OnAttacking()
    {
        _animator.SetTrigger(AnimatorEnemyController.Params.Attacking);
    }

    private void OnDead()
    {
        _animator.SetBool(AnimatorEnemyController.Params.Dead, true);
    }
}
