using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLevelDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    
    private PlayerLevel _level;

    private void OnEnable()
    {
        if (_level != null)
            _level.LevelChanged += ChangeText;
    }

    private void Start()
    {
        _level = Player.Instance.GetComponent<PlayerLevel>();
        _level.LevelChanged += ChangeText;
        _level.Init();
    }

    private void OnDisable()
    {
        if (_level != null)
            _level.LevelChanged -= ChangeText;
    }

    private void ChangeText(int newValue)
    {
        _text.text = newValue.ToString();
    }
}
