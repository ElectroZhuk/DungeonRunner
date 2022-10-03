using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerLevel))]
public class PlayerFighter : MonoBehaviour
{
    private PlayerLevel _playerLevel;
    private bool _canFight;

    public event UnityAction Attacking;
    public event UnityAction Deafeated;

    private void Awake()
    {
        _playerLevel = GetComponent<PlayerLevel>();
        _canFight = true;
    }

    public void ChangeCanFightState(bool state)
    {
        _canFight = state;
    }

    private bool CanBeat(Enemy enemy)
    {
        return _playerLevel.Level >= enemy.Level;
    }

    private void Beat(Enemy enemy)
    {
        if (_playerLevel.Level < enemy.Level)
            throw new UnityException("Player level is less then Enemy level!");

        _playerLevel.ChangeLevel(enemy.Level);
        enemy.Kill();
        Attacking?.Invoke();
    }

    private void Beaten(Enemy enemy)
    {
        enemy.Attack();
        ChangeCanFightState(false);
        Deafeated?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_canFight == false)
            return;

        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (CanBeat(enemy))
                Beat(enemy);
            else
                Beaten(enemy);
        }
    }
}
