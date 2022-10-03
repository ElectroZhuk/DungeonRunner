using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private EnemyAnimations _animations;
    [SerializeField] private AudioSource _attackSound;
    [SerializeField] private AudioSource _deathSound;

    private void OnEnable()
    {
        _animations.EnemyAttacking += OnEnemyAttacking;
        _animations.EnemyDead += OnEnemyDead;
    }

    private void OnDisable()
    {
        _animations.EnemyAttacking -= OnEnemyAttacking;
        _animations.EnemyDead -= OnEnemyDead;
    }

    private void OnEnemyAttacking()
    {
        _attackSound.pitch = Random.Range(0.9f, 1.1f);
        _attackSound.PlayOneShot(_attackSound.clip);
    }

    private void OnEnemyDead()
    {
        _deathSound.pitch = Random.Range(0.9f, 1.1f);
        _deathSound.PlayOneShot(_deathSound.clip);
    }
}
