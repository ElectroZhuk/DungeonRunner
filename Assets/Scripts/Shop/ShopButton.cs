using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopButton : MonoBehaviour
{
    [SerializeField] private Sprite _interactableSprite;
    [SerializeField] private Sprite _notInteractableSprite;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void SetInteractable(bool state)
    {
        if (state == true)
        {
            _button.image.sprite = _notInteractableSprite;
        }
    }
}
