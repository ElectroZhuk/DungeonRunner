using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class BossFight : MonoBehaviour
{
    [SerializeField] private Transform _playerTargetPoint;
    [SerializeField][Min(0.1f)] private float _timeForMove;
    [SerializeField] private Boss _boss;

    private PlayerLevel _playerLevel;
    private PlayerBossFight _playerBossFight;
    private CharacterController _playerController;

    public static BossFight Instance { get; private set; }

    public UnityAction<BossFight> PlayerVictory;
    public UnityAction<BossFight> PlayerDefeated;
    public UnityAction PlayerAttacking;
    public UnityAction BossAttacking;
    public UnityAction AnimationEnded;
    public UnityAction FightStarted;

    private void Awake()
    {
        Instance = this;
    }

    public void StartFight(CharacterController playerController, PlayerLevel playerLevel, PlayerBossFight playerBossFight)
    {
        _playerLevel = playerLevel;
        _playerController = playerController;
        _playerBossFight = playerBossFight;
        StartCoroutine(MovePlayerToTargetPoint());
    }

    public void PlayerAttackDone()
    {
        _boss.Die();
        PlayerVictory?.Invoke(this);
        AnimationEnded?.Invoke();
    }

    public void BossAttackDone()
    {
        PlayerDefeated?.Invoke(this);
        AnimationEnded?.Invoke();
    }

    private IEnumerator MovePlayerToTargetPoint()
    {
        float distance = Vector3.Distance(_playerTargetPoint.transform.position, _playerLevel.transform.position);
        Vector3 movingVector = (_playerTargetPoint.position - _playerLevel.transform.position).normalized;
        movingVector.y = 0;
        float step = distance / (_timeForMove / Time.fixedDeltaTime);
        float elapsedTime = 0;

        while (elapsedTime < _timeForMove)
        {
            _playerController.Move(movingVector * step);

            yield return new WaitForFixedUpdate();

            elapsedTime += Time.fixedDeltaTime;
        }

        Fight();
    }

    private void Fight()
    {
        FightStarted?.Invoke();

        if (_playerLevel.Level >= _boss.Level)
            PlayerWin();
        else
            BossWin();
    }

    private void PlayerWin()
    {
        PlayerAttacking?.Invoke();
    }

    private void BossWin()
    {
        BossAttacking?.Invoke();
    }
}
