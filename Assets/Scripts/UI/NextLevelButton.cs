using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    [SerializeField] private float _delayForNextLevel;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _textGameObject;

    public event UnityAction Enabled;

    private int _nextSceneIndex;

    private void Start()
    {
        BossFight.Instance.PlayerVictory += OnPlayerWin;

        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            _nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        else
            _nextSceneIndex = 0;

        gameObject.SetActive(false);
    }

    private void OnPlayerWin(BossFight bossFight)
    {
        BossFight.Instance.PlayerVictory -= OnPlayerWin;
        Enabled?.Invoke();
        gameObject.SetActive(true);

        if (_nextSceneIndex == 0)
        {
            _textGameObject.SetActive(true);
            _buttonText.text = "В меню";
        }

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

        while (elapsedTime < _delayForNextLevel)
        {
            yield return null;

            elapsedTime += Time.deltaTime;
        }

        SceneTransition.SwitchToScene(_nextSceneIndex);
    }
}
