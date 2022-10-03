using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonTextAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _heightDeltaRatio;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _buttonImage;

    private Button _button;
    private Vector3 _notPressedPosition;
    private Vector3 _pressedPosition;

    private void Start()
    {
        _button = GetComponent<Button>();
        _notPressedPosition = _text.rectTransform.localPosition;
        _pressedPosition = _notPressedPosition - new Vector3(0, (_buttonImage.rectTransform.rect.height * _heightDeltaRatio), 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_button.interactable)
            _text.rectTransform.localPosition = _pressedPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_button.interactable)
            _text.rectTransform.localPosition = _notPressedPosition;
    }
}
