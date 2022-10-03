using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private InGameMenu _menu;
    [Header("Other button")]
    [SerializeField] private PlayButton _playButton;
    [SerializeField] private RestartButton _restartButton;
    [SerializeField] private NextLevelButton _nextLevelButton;

    private void Awake()
    {
        _button.interactable = false;
        _playButton.Disabled += SetButtonInteractable;
        _restartButton.Enabled += SetButtonNotInteractable;
        _nextLevelButton.Enabled += SetButtonNotInteractable;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        _menu.Open(_button);
    }

    private void SetButtonInteractable()
    {
        _button.interactable = true;
    }

    private void SetButtonNotInteractable()
    {
        _button.interactable = false;
    }
}
