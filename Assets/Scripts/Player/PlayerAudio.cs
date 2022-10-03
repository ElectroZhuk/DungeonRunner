using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private PlayerAnimations _animations;
    [SerializeField] private AudioSource _stepAudio;
    [SerializeField] private AudioSource _attackAudio;
    [SerializeField] private AudioSource _deadAudio;

    private float _lastStepPitch = 1;

    private void OnEnable()
    {
        _animations.PlayerStepped += OnStep;
        _animations.PlayerSwordAttack += OnSwordAttack;
        _animations.PlayerDead += OnPlayerDead;
    }

    private void OnDisable()
    {
        _animations.PlayerStepped -= OnStep;
        _animations.PlayerSwordAttack -= OnSwordAttack;
        _animations.PlayerDead -= OnPlayerDead;
    }

    private void OnStep()
    {
        if (_lastStepPitch >= 1)
            _stepAudio.pitch = Random.Range(1f, 1.2f);
        else
            _stepAudio.pitch = Random.Range(0.8f, 1f);

        _lastStepPitch = _stepAudio.pitch;
        _stepAudio.PlayOneShot(_stepAudio.clip);
    }

    private void OnSwordAttack()
    {
        _attackAudio.pitch = Random.Range(0.9f, 1.1f);
        _attackAudio.PlayOneShot(_attackAudio.clip);
    }

    private void OnPlayerDead()
    {
        _deadAudio.PlayOneShot(_deadAudio.clip);
    }
}
