using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightGround : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _finishPoint;

    private Vector3 _movingVector;

    public Vector3 MovingVector => _movingVector;

    private void Start()
    {
        _movingVector = Vector3.Normalize(_finishPoint.position - _startPoint.position);
    }
}
