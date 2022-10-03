using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
public class LinesCollectiblesPackSetter : MonoBehaviour
{
    [SerializeField] private LinesMode _mode;
    [Header("Prefab configuration")]
    [SerializeField] private Collectible _collectiblePrefab;
    [SerializeField][Min(0.1f)] private float _collectibleScale;
    [SerializeField] private Vector3 _collectibleRotation;
    [Header("Lines configuration")]
    [SerializeField][Min(1)] private int _linesAmount;
    [SerializeField][Min(1)] private int _linesLength;
    [Header("Distance configuration")]
    [SerializeField][Min(0.1f)] private float _distanceBeetwenLines;
    [SerializeField][Min(0.1f)] private float _distanceBeetwenCollectibles;
    [SerializeField] private float _heightFromPivot;

    private enum LinesMode
    {
        Parallel,
        LadderToRight,
        LadderToLeft
    }

    [ContextMenu("Create collectibles")]
    private void CreateCollectibles()
    {
        foreach (var collectible in GetComponentsInChildren<Collectible>())
            DestroyImmediate(collectible.gameObject);

        Vector3 startPosition = new Vector3(transform.position.x - (_linesAmount / 2) * _distanceBeetwenLines, transform.position.y + _heightFromPivot, transform.position.z - (_linesLength / 2) * _distanceBeetwenCollectibles);
        
        if (_linesLength % 2 == 0)
            startPosition.z += _distanceBeetwenCollectibles / 2f;

        if (_mode == LinesMode.Parallel)
        {
            for (int line = 0; line < _linesAmount; line++)
            {
                for (int collectible = 0; collectible < _linesLength; collectible++)
                {
                    var sceneCollectible = Instantiate(_collectiblePrefab, new Vector3(startPosition.x + line * _distanceBeetwenLines, startPosition.y, startPosition.z + collectible * _distanceBeetwenCollectibles), Quaternion.Euler(_collectibleRotation), transform);
                    sceneCollectible.transform.localScale = Vector3.one * _collectibleScale;
                }
            }
        }
        else if (_mode == LinesMode.LadderToRight)
        {
            CreateLadder(true, startPosition);
        }
        else if (_mode == LinesMode.LadderToLeft)
        {
            CreateLadder(false, startPosition);
        }
    }

    private void CreateLadder(bool isRightSided, Vector3 startPosition)
    {
        startPosition.z -= _linesLength * _distanceBeetwenCollectibles;

        if (isRightSided)
        {
            for (int line = 0; line < _linesAmount; line++)
            {
                for (int collectible = 0; collectible < _linesLength; collectible++)
                {
                    var sceneCollectible = Instantiate(_collectiblePrefab, startPosition, Quaternion.Euler(_collectibleRotation), transform);
                    sceneCollectible.transform.localScale = Vector3.one * _collectibleScale;
                    startPosition.z += _distanceBeetwenCollectibles;
                }

                startPosition.x += _distanceBeetwenLines;
            }
        }
        else
        {
            startPosition.x += _distanceBeetwenLines * (_linesAmount - 1);

            for (int line = 0; line < _linesAmount; line++)
            {
                for (int collectible = 0; collectible < _linesLength; collectible++)
                {
                    var sceneCollectible = Instantiate(_collectiblePrefab, startPosition, Quaternion.Euler(_collectibleRotation), transform);
                    sceneCollectible.transform.localScale = Vector3.one * _collectibleScale;
                    startPosition.z += _distanceBeetwenCollectibles;
                }

                startPosition.x -= _distanceBeetwenLines;
            }
        }
    }
}
#endif
