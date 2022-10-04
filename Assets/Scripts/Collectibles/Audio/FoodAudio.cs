using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAudio : MonoBehaviour
{
    [SerializeField] private PlayerLevel _level;
    [SerializeField] private AudioSource _goodFoodAudio;
    [SerializeField] private AudioSource _badFoodAudio;

    private float _minPitchRange = 0.9f;
    private float _maxPitchRange = 1.1f;

    private void OnEnable()
    {
        _level.LevelIncreased += OnGoodFoodCollected;
        _level.LevelDecreased += OnBadFoodCollected;
    }

    private void OnDisable()
    {
        _level.LevelIncreased -= OnGoodFoodCollected;
        _level.LevelDecreased -= OnBadFoodCollected;
    }

    private void OnGoodFoodCollected()
    {
        _goodFoodAudio.pitch = Random.Range(_minPitchRange, _maxPitchRange);
        _goodFoodAudio.PlayOneShot(_goodFoodAudio.clip);
    }

    private void OnBadFoodCollected()
    {
        _badFoodAudio.pitch = Random.Range(_minPitchRange, _maxPitchRange);
        _badFoodAudio.PlayOneShot(_badFoodAudio.clip);
    }
}
