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
    private Vector2 _attackAudioPitch = new Vector2(0.9f, 1.1f);
    private Vector2 _rightStepAudioPitch = new Vector2(1f, 1.2f);
    private Vector2 _leftStepAudioPitch = new Vector2(0.8f, 1f);

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
            _stepAudio.pitch = Random.Range(_rightStepAudioPitch.x, _rightStepAudioPitch.y);
        else
            _stepAudio.pitch = Random.Range(_leftStepAudioPitch.x, _leftStepAudioPitch.y);

        _lastStepPitch = _stepAudio.pitch;
        _stepAudio.PlayOneShot(_stepAudio.clip);
    }

    private void OnSwordAttack()
    {
        _attackAudio.pitch = Random.Range(_attackAudioPitch.x, _attackAudioPitch.y);
        _attackAudio.PlayOneShot(_attackAudio.clip);
    }

    private void OnPlayerDead()
    {
        _deadAudio.PlayOneShot(_deadAudio.clip);
    }
}
