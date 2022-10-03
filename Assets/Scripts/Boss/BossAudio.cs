using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _attackAudio;
    [SerializeField] private AudioSource _deathAudio;
    [SerializeField] private AudioSource _sleepAudio;


    public void OnBossAttack()
    {
        _attackAudio.Play();
    }

    public void OnBossDeath()
    {
        _deathAudio.Play();
    }

    public void OnBossSleeping()
    {
        _sleepAudio.Play();
    }
}
