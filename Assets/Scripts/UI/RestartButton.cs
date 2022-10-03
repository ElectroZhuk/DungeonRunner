using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private float _timeForRestart;
    [SerializeField] private Button _button;

    public event UnityAction Enabled;

    private void Start()
    {
        Player.Instance.Dead += OnPlayerDead;
        BossFight.Instance.PlayerDefeated += OnPlayerDead;
        gameObject.SetActive(false);
    }

    private void OnPlayerDead()
    {
        BossFight.Instance.PlayerDefeated -= OnPlayerDead;
        Player.Instance.Dead -= OnPlayerDead;
        Enabled?.Invoke();
        gameObject.SetActive(true);
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnPlayerDead(BossFight bossFight)
    {
        BossFight.Instance.PlayerDefeated -= OnPlayerDead;
        Player.Instance.Dead -= OnPlayerDead;
        Enabled?.Invoke();
        gameObject.SetActive(true);
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        _button.interactable = false;
        _button.onClick.RemoveListener(OnButtonClicked);
        StartCoroutine(WaitForRestart());
    }

    private IEnumerator WaitForRestart()
    {
        float elapsedTime = 0;

        while (elapsedTime < _timeForRestart)
        {
            yield return null;

            elapsedTime += Time.deltaTime;
        }

        SceneTransition.SwitchToScene(SceneManager.GetActiveScene().name);
    }
}
