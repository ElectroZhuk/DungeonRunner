using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private SavedLevelLoader _savedLevelLoader;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClicked);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void OnStartButtonClicked()
    {
        _startButton.interactable = false;
        _savedLevelLoader.LoadSavedLevel();
    }
}
