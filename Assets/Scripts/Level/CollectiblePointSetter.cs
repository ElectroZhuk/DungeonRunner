using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
[ExecuteAlways]
public class CollectiblePointSetter : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField][Min(1)] private int _linesAmount;
    [SerializeField][Min(0)] private float _distanceBetweenLines;
    [SerializeField][Min(0)] private float _distanceBetweenCollectibles;
    [Header("Points")]
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _finishPoint;
    [SerializeField] private CollectiblePoint _collectiblePointPrefab;
    [SerializeField] private Transform _pointContainer;

    private void Start()
    {
        foreach (CollectiblePoint collectiblePoint in GetComponentsInChildren<CollectiblePoint>())
            collectiblePoint.SetPoints(_startPoint, _finishPoint);
    }

    [ContextMenu("Create collectible lines")]
    private void CreateCollectiblesLines()
    {
        if (_startPoint == null || _finishPoint == null)
            throw new UnityException("Points can't be null!");

        foreach (CollectiblePoint point in gameObject.GetComponentsInChildren<CollectiblePoint>())
            DestroyImmediate(point);

        Vector3 firstCollectiblePoint = _startPoint.localPosition + new Vector3(_distanceBetweenCollectibles / 2f, 0, 0);

        if (_linesAmount % 2 == 0)
            firstCollectiblePoint += new Vector3(0, 0, (_linesAmount / 2 - 1) * _distanceBetweenLines + _distanceBetweenLines / 2f);
        else
            firstCollectiblePoint += new Vector3(0, 0, (_linesAmount / 2) * _distanceBetweenLines);

        for (var lineNumber = 0; lineNumber < _linesAmount; lineNumber++)
        {
            Vector3 startPosition = firstCollectiblePoint - new Vector3(0, 0, _distanceBetweenLines * lineNumber);

            while (startPosition.x < _finishPoint.localPosition.x)
            {
                CollectiblePoint point = Instantiate(_collectiblePointPrefab, startPosition, Quaternion.identity, _pointContainer);
                point.transform.localPosition = startPosition;
                point.SetPoints(_startPoint, _finishPoint);
                point.gameObject.layer = 3;
                startPosition += new Vector3(_distanceBetweenCollectibles, 0, 0);
            }
        }
    }
}
#endif
