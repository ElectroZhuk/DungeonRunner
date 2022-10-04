using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private int _level;

    public int Level => _level;

    public event UnityAction<int> LevelChanged;
    public event UnityAction LevelIncreased;
    public event UnityAction LevelDecreased;

    private int _minLevel = 1;

    public void Init()
    {
        LevelChanged?.Invoke(Level);
    }

    public void ChangeLevel(int levelDelta)
    {
        _level += levelDelta;

        if (levelDelta < 0)
            LevelDecreased?.Invoke();
        else
            LevelIncreased?.Invoke();

        if (_level < _minLevel)
            _level = _minLevel;

        LevelChanged?.Invoke(Level);
    }

    public bool CanAdd() => true;
}
