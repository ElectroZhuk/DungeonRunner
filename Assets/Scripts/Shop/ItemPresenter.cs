using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemPresenter : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _buttonCannotPress;
    [SerializeField] private Sprite _buttonCanPress;

    private Item _item;
    private Vector3 _notPressedTextPosition;
    private Vector3 _pressedTextPosition;
    private float _heightDeltaRatio = 0.18f;

    public UnityAction<Item, ItemPresenter> ButtonClicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    public void Init(Item item)
    {
        if (item is AvatarItem avatar)
            avatar.Init();

        _notPressedTextPosition = _text.rectTransform.localPosition;
        _pressedTextPosition = _notPressedTextPosition - new Vector3(0, (_button.image.rectTransform.rect.height * _heightDeltaRatio), 0);
        _item = item;
        _icon.sprite = item.Icon;
        UpdateSavedInfo();
    }

    public void UpdateSavedInfo()
    {
        if (_item.IsBought() == false)
        {
            _button.image.sprite = _buttonCanPress;
            _button.interactable = true;
            _text.text = _item.Cost.ToString();
            _text.rectTransform.localPosition = _notPressedTextPosition;
            return;
        }

        if (_item is AvatarItem avatar)
        {
            if (avatar.IsSelected())
            {
                _button.image.sprite = _buttonCannotPress;
                _button.interactable = false;
                _text.text = "Выбрано";
                _text.rectTransform.localPosition = _pressedTextPosition;
            }
            else
            {
                _button.image.sprite = _buttonCanPress;
                _button.interactable = true;
                _text.text = "Выбрать";
                _text.rectTransform.localPosition = _notPressedTextPosition;
            }
        }
    }

    private void OnButtonClicked()
    {
        ButtonClicked?.Invoke(_item, this);
    }
}
