using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private Animator _animator;
    [SerializeField] private BossFight _fightField;
    [SerializeField] private ParticleSystem _fire;

    public float Level => _level;

    public UnityAction<int> LevelChanged;

    private void Start()
    {
        LevelChanged?.Invoke(_level);
    }

    private void OnEnable()
    {
        _fightField.BossAttacking += OnBossAttacking;
    }

    private void OnDisable()
    {
        _fightField.BossAttacking -= OnBossAttacking;
    }

    public void BossAttackDone()
    {
        _fightField.BossAttackDone();
    }

    public void StartFire()
    {
        _fire.Play();
    }

    public void StopFire()
    {
        _fire.Stop();
    }

    public void Die()
    {
        _animator.SetTrigger(AnimatorTerrorBringerController.Params.Lose);
    }

    private void OnBossAttacking()
    {
        _animator.SetTrigger(AnimatorTerrorBringerController.Params.Win);
    }
}
