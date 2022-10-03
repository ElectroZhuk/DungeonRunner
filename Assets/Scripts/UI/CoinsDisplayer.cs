using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private PlayerMoney _coins;

    private void OnEnable()
    {
        if (_coins != null)
            _coins.CoinsChanged += ChangeText;
    }

    private void Start()
    {
        _coins = Player.Instance.GetComponent<PlayerMoney>();
        _coins.CoinsChanged += ChangeText;
    }

    private void OnDisable()
    {
        if (_coins != null)
            _coins.CoinsChanged -= ChangeText;
    }

    private void ChangeText(int newValue)
    {
        _text.text = newValue.ToString();
    }
}
