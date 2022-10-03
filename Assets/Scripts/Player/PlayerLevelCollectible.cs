using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[RequireComponent(typeof(PlayerLevel))]
public class PlayerLevelCollectible : MonoBehaviour
{
    private PlayerLevel _level;

    private void Awake()
    {
        _level = GetComponent<PlayerLevel>();
    }

    public void Add(LevelCollectible levelCollectible)
    {
        if (levelCollectible == null)
            throw new UnityException("Element can't be null");

        _level.ChangeLevel(levelCollectible.NumberOfLevels);
    }

    public bool CanAdd(LevelCollectible levelCollectible) => true;
}
