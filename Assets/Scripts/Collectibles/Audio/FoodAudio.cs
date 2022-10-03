using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAudio : MonoBehaviour
{
    [SerializeField] private PlayerLevel _level;
    [SerializeField] private AudioSource _goodFoodAudio;
    [SerializeField] private AudioSource _badFoodAudio;

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
        _goodFoodAudio.pitch = Random.Range(0.9f, 1.1f);
        _goodFoodAudio.PlayOneShot(_goodFoodAudio.clip);
    }

    private void OnBadFoodCollected()
    {
        _badFoodAudio.pitch = Random.Range(0.9f, 1.1f);
        _badFoodAudio.PlayOneShot(_badFoodAudio.clip);
    }
}
