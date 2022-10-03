using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixStartPosition : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private bool _isXFixed;
    [SerializeField] private bool _isYFixed;
    [SerializeField] private bool _isZFixed;

    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = _targetTransform.localPosition;
    }

    private void Update()
    {
        Vector3 targetPosition = _targetTransform.localPosition;

        if (_isXFixed)
            targetPosition.x = _startPosition.x;

        if (_isYFixed)
            targetPosition.y = _startPosition.y;

        if (_isZFixed)
            targetPosition.z = _startPosition.z;

        _targetTransform.localPosition = targetPosition;
    }
}
