using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerFighter _fighter;
    [SerializeField] private PlayerBossFight _bossFight;
    [SerializeField] private ParticleSystem _runParticle;
    [SerializeField] private ParticleSystem _attackHitParticle;
    [SerializeField] private AnimationClip _jumpEnd;
    [SerializeField] private float _runningDistanceTreshold;

    private Animator _animator;

    public UnityAction PlayerStepped;
    public UnityAction PlayerSwordAttack;
    public UnityAction PlayerDead;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _player.Dead += OnDead;
        _movement.Running += OnRunning;
        _fighter.Attacking += OnAttacking;
        _movement.JumpStarted += OnJumpStarted;
        _bossFight.Win += OnPlayerWin;
        _bossFight.Defeated += OnBossWin;
        _bossFight.PlayerAttacking += OnBossFightAttacking;
        _bossFight.Started += OnStay;
    }

    private void OnDisable()
    {
        _player.Dead -= OnDead;
        _movement.Running -= OnRunning;
        _fighter.Attacking -= OnAttacking;
        _movement.JumpStarted -= OnJumpStarted;
        _bossFight.Win -= OnPlayerWin;
        _bossFight.Defeated -= OnBossWin;
        _bossFight.PlayerAttacking -= OnBossFightAttacking;
        _bossFight.Started -= OnStay;
    }

    public void PlayerAttackDone()
    {
        _bossFight.PlayerAttackDone();
    }

    public void OnAttackAnimationHitMoment()
    {
        _attackHitParticle.Play();
    }

    public void OnPlayerStep()
    {
        PlayerStepped?.Invoke();
    }

    public void OnPlayerSwordAttack()
    {
        PlayerSwordAttack?.Invoke();
    }

    public void OnPlayerDead()
    {
        PlayerDead?.Invoke();
    }

    private void OnBossFightAttacking()
    {
        _animator.SetTrigger(AnimatorPlayerController.Params.AttackingBoss);
    }

    private void OnStay()
    {
        _animator.SetBool(AnimatorPlayerController.Params.Running, false);
        _runParticle.Stop();
    }

    private void OnDead()
    {
        _animator.SetTrigger(AnimatorPlayerController.Params.Dead);
        _runParticle.Stop();
    }

    private void OnRunning(float distance)
    {
        bool isRunning = distance >= _runningDistanceTreshold;
        _animator.SetBool(AnimatorPlayerController.Params.Running, isRunning);

        if (isRunning && _runParticle.isPlaying == false)
            _runParticle.Play();
    }

    private void OnAttacking()
    {
        _animator.SetTrigger(AnimatorPlayerController.Params.Attacking);
    }

    private void OnJumpStarted(Jumper jumper)
    {
        _animator.SetTrigger(AnimatorPlayerController.Params.JumpStart);
        jumper.JumpProcess += ProcessJump;
    }

    private void OnPlayerWin()
    {
        _animator.SetTrigger(AnimatorPlayerController.Params.Win);
    }

    private void OnBossWin()
    {
        _animator.SetTrigger(AnimatorPlayerController.Params.Lose);
    }

    private void ProcessJump(float process, Jumper jumper)
    {
        _animator.SetFloat(AnimatorPlayerController.Params.JumpProcess, process);

        if (process <= _jumpEnd.length * 0.95f)
        {
            jumper.JumpProcess -= ProcessJump;
            OnJumpEnding(jumper);
        }
    }

    private void OnJumpEnding(Jumper jumper)
    {
        _animator.SetTrigger(AnimatorPlayerController.Params.JumpEnd);
    }
}
