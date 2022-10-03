using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    private Button _openButton;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(Close);
        _restartButton.onClick.AddListener(RestartLevel);
        _exitButton.onClick.AddListener(Exit);
        _resumeButton.onClick.AddListener(SetButtonsNotInteractable);
        _restartButton.onClick.AddListener(SetButtonsNotInteractable);
        _exitButton.onClick.AddListener(SetButtonsNotInteractable);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(Close);
        _restartButton.onClick.RemoveListener(RestartLevel);
        _exitButton.onClick.RemoveListener(Exit);
        _resumeButton.onClick.RemoveListener(SetButtonsNotInteractable);
        _restartButton.onClick.RemoveListener(SetButtonsNotInteractable);
        _exitButton.onClick.RemoveListener(SetButtonsNotInteractable);
    }

    public void Open(Button openButton)
    {
        gameObject.SetActive(true);
        _resumeButton.interactable = true;
        _restartButton.interactable = true;
        _exitButton.interactable = true;
        Time.timeScale = 0;
        _openButton = openButton;
        _openButton.interactable = false;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        _openButton.interactable = true;
        Time.timeScale = 1;
    }

    private void RestartLevel()
    {
        Time.timeScale = 1;
        SceneTransition.SwitchToScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Exit()
    {
        Time.timeScale = 1;
        SceneTransition.SwitchToScene(0);
    }

    private void SetButtonsNotInteractable()
    {
        _resumeButton.interactable = false;
        _restartButton.interactable = false;
        _exitButton.interactable = false;
    }
}
