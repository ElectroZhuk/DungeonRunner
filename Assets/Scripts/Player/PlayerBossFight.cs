using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerLevel), typeof(PlayerMovement))]
public class PlayerBossFight : MonoBehaviour
{
    private PlayerLevel _level;
    private PlayerMovement _movement;
    private BossFight _bossFight;

    public UnityAction<BossFight> Entered;
    public UnityAction Started;
    public UnityAction Win;
    public UnityAction Defeated;
    public UnityAction PlayerAttacking;

    private void Awake()
    {
        _level = GetComponent<PlayerLevel>();
        _movement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BossFight>(out BossFight bossFight))
        {
            _bossFight = bossFight;
            Entered?.Invoke(bossFight);
            _movement.ChangeRunningState(false);
            bossFight.PlayerAttacking += OnPlayerAttacking;
            bossFight.PlayerVictory += OnPlayerWin;
            bossFight.PlayerDefeated += OnPlayerDefeated;
            bossFight.FightStarted += OnFightStarted;
            bossFight.StartFight(_movement.Controller,_level, this);
        }
    }

    public void PlayerAttackDone()
    {
        if (_bossFight != null)
            _bossFight.PlayerAttackDone();
    }

    private void OnFightStarted()
    {
        _bossFight.FightStarted -= OnFightStarted;
        Started?.Invoke();
    }

    private void OnPlayerAttacking()
    {
        PlayerAttacking?.Invoke();
    }

    private void OnPlayerWin(BossFight fight)
    {
        fight.PlayerVictory -= OnPlayerWin;
        fight.PlayerDefeated -= OnPlayerDefeated;
        Win?.Invoke();
    }

    private void OnPlayerDefeated(BossFight fight)
    {
        fight.PlayerDefeated -= OnPlayerDefeated;
        fight.PlayerVictory -= OnPlayerWin;
        Defeated?.Invoke();
    }
}
