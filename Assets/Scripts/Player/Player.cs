using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerContact), typeof(PlayerFighter))]
[RequireComponent(typeof(PlayerBossFight), typeof(PlayerMoney))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private PlayerMovement _movement;
    private PlayerContact _contact;
    private PlayerFighter _fighter;
    private PlayerMoney _money;
    private PlayerBossFight _bossFight;

    public event UnityAction Win;
    public event UnityAction Dead;

    private void Awake()
    {
        Instance = this;
        _movement = GetComponent<PlayerMovement>();
        _contact = GetComponent<PlayerContact>();
        _fighter = GetComponent<PlayerFighter>();
        _money = GetComponent<PlayerMoney>();
        _bossFight = GetComponent<PlayerBossFight>();
    }

    private void OnEnable()
    {
        _contact.Trapped += OnDead;
        _fighter.Deafeated += OnDead;
        _bossFight.Win += OnPlayerWin;
        _bossFight.Defeated += OnDead;
    }

    private void OnDisable()
    {
        _contact.Trapped -= OnDead;
        _fighter.Deafeated -= OnDead;
        _bossFight.Win -= OnPlayerWin;
        _bossFight.Defeated -= OnDead;
    }

    public void Activate()
    {
        _movement.Activate();
    }

    private void OnPlayerWin()
    {
        SaveCurrentMoney();
        SaveCurrentLevel();
    }

    private void SaveCurrentMoney()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsNames.Keys.Money))
            PlayerPrefs.SetInt(PlayerPrefsNames.Keys.Money, PlayerPrefs.GetInt(PlayerPrefsNames.Keys.Money) + _money.CoinsAmount);
        else
            PlayerPrefs.SetInt(PlayerPrefsNames.Keys.Money, _money.CoinsAmount);

        PlayerPrefs.Save();
    }

    private void SaveCurrentLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            PlayerPrefs.SetInt(PlayerPrefsNames.Keys.Level, SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.Save();
        }
    }

    private void OnDead()
    {
        _movement.Deactivate();
        _fighter.ChangeCanFightState(false);
        Dead?.Invoke();
    }
}
