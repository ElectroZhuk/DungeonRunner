using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] [Min(0)] private float _waitBeforeStart;
    [SerializeField] private Button _button;

    public event UnityAction Disabled;

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
        _button.interactable = false;
        StartCoroutine(WaitForStart());
    }

    private IEnumerator WaitForStart()
    {
        float elapsedTime = 0;

        while (elapsedTime < _waitBeforeStart)
        {
            yield return null;

            elapsedTime += Time.deltaTime;
        }

        Disabled?.Invoke();
        gameObject.SetActive(false);
        Player.Instance.Activate();
    }
}
