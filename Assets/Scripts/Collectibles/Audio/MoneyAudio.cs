using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyAudio : MonoBehaviour
{
    [SerializeField] private PlayerMoney _money;
    [SerializeField] private AudioSource _collectedAudio;

    private void OnEnable()
    {
        _money.CoinsChanged += OnCollected;
    }

    private void OnDisable()
    {
        _money.CoinsChanged -= OnCollected;
    }

    private void OnCollected(int amount)
    {
        if (amount > 0)
        {
            _collectedAudio.pitch = Random.Range(0.9f, 1.1f);
            _collectedAudio.PlayOneShot(_collectedAudio.clip);
        }
    }
}
