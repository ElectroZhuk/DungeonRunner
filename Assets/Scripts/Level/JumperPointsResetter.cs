using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
[ExecuteAlways()]
public class JumperPointsResetter : MonoBehaviour
{
    [Header("Snap")]
    [SerializeField] private Transform _startSnapPoint;
    [SerializeField] private Transform _finishSnapPoint;
    [Header("Jump")]
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _finishPoint;
    [Header("Config")]
    [SerializeField] private float _jumpPointOffset;
    [Header("Colliders")]
    [SerializeField] private BoxCollider _enterTrigger;

    private float _lastJumpPointOffset;
    private Vector3 _lastTransformScale;
    private BoxCollider[] _colliders;

    private void Start()
    {
        _lastTransformScale = transform.localScale;
        _lastJumpPointOffset = _jumpPointOffset;
        _colliders = GetComponents<BoxCollider>().Where(collider => collider.isTrigger == false).ToArray();
    }

    private void Update()
    {
        if (_lastTransformScale != transform.localScale || _lastJumpPointOffset != _jumpPointOffset)
        {
            ResetJumpPoints();
            _lastTransformScale = transform.localScale;
            _lastJumpPointOffset = _jumpPointOffset;
        }
    }

    [ContextMenu("Reset jump points")]
    private void ResetJumpPoints()
    {
        _startPoint.position = _startSnapPoint.position + (_startSnapPoint.position - _finishPoint.position).normalized * _jumpPointOffset;
        _enterTrigger.center = _startPoint.localPosition + new Vector3(0, _enterTrigger.size.y / 2f);
        _finishPoint.position = _finishSnapPoint.position + (_finishSnapPoint.position - _startPoint.position).normalized * _jumpPointOffset;

        foreach (var collider in GetComponents<BoxCollider>().Where(collider => collider.isTrigger == false).ToArray())
        {
            collider.size = new Vector3(collider.size.x, collider.size.y, Vector3.Distance(_startPoint.position, _finishPoint.position) / transform.localScale.z);
        }
    }
}
#endif
