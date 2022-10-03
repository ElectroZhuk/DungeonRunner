using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossLevelDisplayer : MonoBehaviour
{
    [SerializeField] private Boss _boss;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _boss.LevelChanged += ChangeLevelValue;
    }

    private void OnDisable()
    {
        _boss.LevelChanged -= ChangeLevelValue;
    }

    private void ChangeLevelValue(int currentLevel)
    {
        _text.text = currentLevel.ToString() + " lvl";
    }
}
