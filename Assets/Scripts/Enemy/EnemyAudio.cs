using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private EnemyAnimations _animations;
    [SerializeField] private AudioSource _attackSound;
    [SerializeField] private AudioSource _deathSound;

    private float _minPitchRange = 0.9f;
    private float _maxPitchRange = 1.1f;

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
        _attackSound.pitch = Random.Range(_minPitchRange, _maxPitchRange);
        _attackSound.PlayOneShot(_attackSound.clip);
    }

    private void OnEnemyDead()
    {
        _deathSound.pitch = Random.Range(_minPitchRange, _maxPitchRange);
        _deathSound.PlayOneShot(_deathSound.clip);
    }
}
