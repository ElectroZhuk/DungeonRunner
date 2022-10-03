using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Slide : MonoBehaviour
{
    [SerializeField] private Button _changeButton;

    public event UnityAction Switching;

    private void OnEnable()
    {
        _changeButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _changeButton.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        Switching?.Invoke();
    }
}
