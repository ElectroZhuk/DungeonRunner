using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class Jumper : PlayerExternalControl
{
    [Header("Config")]
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _finishPoint;
    [SerializeField] [Min(1)] float _height;
    [SerializeField] [Min(1)] float _timeInAir;
    [SerializeField] private AnimationCurve _jumpCurve;

    private float _playerHalfHeight;

    public UnityAction<float, Jumper> JumpProcess;

    public float Speed => Vector3.Distance(_startPoint.position, _finishPoint.position) / _timeInAir;

    public void StartJump(CharacterController player)
    {
        _playerHalfHeight = player.height / 2f;

        foreach (var collider in GetComponents<Collider>().Where(component => component.isTrigger == false))
            collider.enabled = true;

        StartCoroutine(Jump(player));
    }

    private IEnumerator Jump(CharacterController player)
    {
        Vector3 distanceVector = _finishPoint.position - _startPoint.position;
        float distancePerSecond = distanceVector.magnitude / _timeInAir;
        float elapsedTime = 0;
        float travelledLength = 0;

        while (elapsedTime < _timeInAir)
        {
            JumpProcess?.Invoke(_timeInAir - elapsedTime, this);
            travelledLength += distancePerSecond * Time.fixedDeltaTime;
            Vector3 nextStepAlongJump = distanceVector.normalized * distancePerSecond * Time.fixedDeltaTime;
            Vector3 nextStepUp = new Vector3(0, _jumpCurve.Evaluate(travelledLength / Vector3.Distance(_startPoint.position, _finishPoint.position)) * (_height + _playerHalfHeight) - player.transform.position.y);
            Vector3 nextPoint = (nextStepAlongJump + nextStepUp + player.transform.position);
            player.Move(nextPoint - player.transform.position);
            
            yield return new WaitForFixedUpdate();

            elapsedTime += Time.fixedDeltaTime;
        }

        EndJump();
    }

    private void EndJump()
    {
        foreach (var collider in GetComponents<Collider>().Where(component => component.isTrigger == false))
            collider.enabled = false;

        ControlEnded?.Invoke(this);
    }
}
