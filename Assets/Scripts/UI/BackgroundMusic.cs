using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _music;

    private void OnEnable()
    {
        _music.Play();
    }

    private void OnDisable()
    {
        _music.Stop();
    }
}
