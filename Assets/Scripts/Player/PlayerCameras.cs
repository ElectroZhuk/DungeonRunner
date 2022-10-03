using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCameras : MonoBehaviour
{
    [SerializeField] private Animator _camerasAnimator;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.Dead += OnPlayerDead;
    }

    private void OnDisable()
    {
        _player.Dead -= OnPlayerDead;
    }

    private void OnPlayerDead()
    {
        _camerasAnimator.SetBool(PlayerCameraSwitcherAnimatorController.Params.Dead, true);
    }
}
