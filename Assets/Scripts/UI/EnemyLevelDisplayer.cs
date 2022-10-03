using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyLevelDisplayer : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _enemy.LevelChanged += ChangeLevelValue;
    }

    private void OnDisable()
    {
        _enemy.LevelChanged -= ChangeLevelValue;
    }

    private void ChangeLevelValue(int currentLevel)
    {
        _text.text = currentLevel.ToString() + " lvl";
    }
}
